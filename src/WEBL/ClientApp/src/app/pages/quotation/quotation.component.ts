import { Component, Inject, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DxDataGridComponent } from 'devextreme-angular';
import { DxoLookupComponent } from 'devextreme-angular/ui/nested';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import { exportDataGrid } from 'devextreme/excel_exporter';
import notify from 'devextreme/ui/notify';
import { Workbook } from 'exceljs';
import saveAs from 'file-saver';
import { currencyZAR, deniedStatus, pendingStatus, reviewStatus, pendingMonitoredStatus, draftStatus, draft, completed } from 'src/globalConstants';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { StockService } from '../../core/services/stock.service';
import { OrdersService } from '../../core/services/orders.service';
import { PriceIncreaseService } from '../../core/services/priceIncrease.service';
import { PriceLookUpService } from '../../core/services/pricelookup.service';
import { priceIncrease } from '../../core/models/priceIncrease.models';

@Component({
  selector: 'app-quotation',
  templateUrl: './quotation.component.html',
  styleUrls: ['./quotation.component.scss']
})
export class QuotationComponent implements OnInit, OnDestroy {
  dataSource: any;
  priceIncrease: priceIncrease = new priceIncrease();
  UsersForeignDataSource: any;
  DepartmentManagersForeignDataSource: any;
  SupplierForeignDataSource: any;
  PlantLocationForeignDataSource: any;
  DepartmentForeignDataSource: any;
  StockForeignDataSource: any;
  GLCodeForeignDataSource: any;
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  pageName: string = "Quotation";
  event: any;
  CancelDownload = true;
  popupDownloadConfirmVisible = false;
  popupVisiblePriceIncrease = false;
  allowModify = false;
  baseUrl: string;
  currentDeleteItem: any;
  popupDeleteItemtVisible: boolean = false;
  savecolumn = false;
  popupVisible = false;
  currencyZAR: any;
  userId: any;
  columnIndex: any;
  selectedRowIndex = -1;
  heading:any;
  statuses: string[];
  defaultVisible = false;
  allowEdit = true;
  allowRedirect = false;
  quotationId: any;
  internalComment: any;
  loadingVisible = false;
  hasComment = false;
  department: any;
  rowIndex = 0;
  action: any;
  priceIncreases: any;
  popupquestion = false;
  ProductDropdown: any;
  ProductForeignDataSource: any;
  CustomerForeignDataSource: any;
  QuotationForeignDataSource: any;
  revision: number;
  noDownloadButtonOptions: any = {
    text: "No",
    onClick: (e) => {
      this.popupDownloadConfirmVisible = false;
      this.CancelDownload = true;

    }
  };

  yesDownloadButtonOptions = {
    text: "Yes",
    onClick: () => {
      this.popupDownloadConfirmVisible = false;

      const workbook = new Workbook();
      const worksheet = workbook.addWorksheet('Main sheet');
      exportDataGrid({
        component: this.event.component,
        worksheet: worksheet
      }).then(function () {
        workbook.xlsx.writeBuffer()
          .then(function (buffer: BlobPart) {
            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Orders-List.xlsx');
          });
      });
    }
  };

  editOption: any = {
    text: "Edit Quotation",
    onClick: (e) => {
      this.router.navigate(['/quotation-modify'], { state: { id: this.quotationId, action: this.action } });
      this.popupquestion = false;
    }
  }

  @ViewChild('content') content;
  @ViewChild("quote", { static: false }) grid: DxDataGridComponent;
    data: any;
    vatperc: number;
  constructor(@Inject('BASE_URL') baseUrl: string, private pricelookupService: PriceLookUpService,private modalService: NgbModal, private orderService: OrdersService, private priceIncreaseService: PriceIncreaseService,private globalMethodsService: GlobalMethodsService, private authService: AuthenticationService, private stockService: StockService, private router: Router, private route: ActivatedRoute,) {
    this.baseUrl = baseUrl;
    this.currencyZAR = currencyZAR;
    this.allowEdit = true;
    this.allowRedirect = false;
    this.hasComment = false;
    this.rowIndex = 0;
    this.heading = "Quotation";
    this.statuses = ['All','Completed','Draft']; 

    this.authService.currentUserSubject.subscribe(user => {
      if (user) {
        this.userId = user.id
      }
    })

    this.dataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Quotation',
        updateUrl: baseUrl + 'api/Quotation',
        insertUrl: baseUrl + 'api/Quotation',
        deleteUrl: baseUrl + 'api/Quotation',
        onInserted: () => {          
          this.globalMethodsService.ToastInsertSuccessMessage(this.pageName);          
        },
        onUpdated: () => {          
          this.globalMethodsService.ToastUpdateSuccessMessage(this.pageName);
        },
        onRemoved: () => {
          this.globalMethodsService.ToastDeleteSuccessMessage(this.pageName);
        }
        /*onBeforeSend: (method, param) => {
      }),
      paginate: true,
    }

    this.UsersForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/FullEmployeeName',
        /*onBeforeSend: (method, param) => {
          param.headers = {
            'Authorization': 'Bearer ' + this.token
          };
        }*/
      }),
      paginate: true
    }

    this.ProductForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Products'
      }),
      paginate: true
    }
    //For Customer
    this.CustomerForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Customer',
      }),
      sort: [
        { selector: "CompanyName", desc: false }
      ],
      paginate: true
    }

    this.ProductDropdown = {
      onValueChanged: (e: { value: number; }) => {
        this.grid.instance.cellValue(this.rowIndex, "productId", e.value);
      }
    }
    this.allowModify = this.authService.HavePermission(109);
  }

  ngOnInit() {

    /**
     * horizontal-vertical layput set
     */
     const attribute = document.body.getAttribute('data-layout');

     this.isVisible = attribute;
     const vertical = document.getElementById('layout-vertical');
     if (vertical != null) {
       vertical.setAttribute('checked', 'true');
     }
     if (attribute == 'horizontal') {
       const horizontal = document.getElementById('layout-horizontal');
       if (horizontal != null) {
         horizontal.setAttribute('checked', 'true');
         console.log(horizontal);
       }
     }

  }

  ngOnDestroy() {
    try {
      this.grid.instance.dispose();
    } catch (e) {

    }
    
  }

  onExporting(e) {
    this.event = e;
    this.popupDownloadConfirmVisible = true;
    if (this.CancelDownload) {
      e.cancel = true;
    }
  }

  onExported(e) {
    notify("Successfully Downloaded", 'success', 5000);
  }

  editRow() {    
    this.grid.instance.editRow(this.selectedRowIndex);
    this.grid.instance.deselectAll();
  }

  deleteRow() {
    this.grid.instance.deleteRow(this.selectedRowIndex);
    this.grid.instance.deselectAll();
  }

  addRow() {
    this.grid.instance.addRow();
    this.grid.instance.deselectAll();
  }

  getFullName(rowData) {
    return rowData.companyName;
  }

  getStock(rowData) {
    if (rowData) {
      return rowData.code + " - " + rowData.productName + " - " + rowData.internalProductName;
    } else {
      return "";
    }
  }

  toggleDefault() {
    this.defaultVisible = !this.defaultVisible;
  }

/* Internal Order */
  selectedChanged(e) {    
    this.selectedRowIndex = e.component.getRowIndexByKey(e.selectedRowKeys[0]);
    this.allowEdit = true;
    this.allowRedirect = false;

    if (e.selectedRowsData[0].quoteStatusId == draft) {
      this.quotationId = e.selectedRowsData[0].id;
      this.action = 2;
      this.router.navigate(['/quotation-modify'], { state: { id: this.quotationId, action: this.action } });
    }
    else if (e.selectedRowsData[0].quoteStatusId == completed) {
      this.quotationId = e.selectedRowsData[0].id;
      this.action = 2;
      this.router.navigate(['/quotation-modify'], { state: { id: this.quotationId, action: this.action } });
    }

  }

  onCellPrepared(e) {
    if (e.rowType === 'data') {
      e.cellElement.style.backgroundColor = "#ffffff";
    }
  }

  onInitNewRow(e) {
    e.data.placedById = this.userId;
    e.data.requestedById = this.userId;
    e.data.deliveryCost = 0;
    e.data.total = 0;
    e.data.discount = 0;
    e.data.internalOrderItems = [];
    this.heading = "New Quotation"; 
  }

  onEditorPreparingParent(e) {
    if (e.parentType === "dataRow") {
      switch (e.dataField) {
        case "requestedById":
        case "placedById": {
          e.editorOptions.onFocusIn = function (args) {
            setTimeout(function () {
              if (!(args.component.option('opened'))) {
                args.component.open();
              }
            }, 200)
          }
          break;
        }
      }
    }
  }

  onSavedParent(e) {
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() =>
      this.router.navigate(['/quotation'])
    );
  }

  onEditCanceledParent(e) {
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() =>
      this.router.navigate(['/quotation'])
    );
  }

  onSaving(e) {
    if (this.grid.instance.hasEditData() == true) {    
      e.cancel = true;
      notify("Make sure all Interal Order Items are saved.", 'error', 5000);
    } 

    if (this.grid.instance.getVisibleRows().length <= 0) {     
      e.cancel = true;
      notify("The Internal Order should have a minimum of one item.", 'error', 5000);
    }
    this.rowIndex = 0;
  }

  onEditingStartParent(e) {
    this.heading = "Quotation " + e.data.id;
    this.rowIndex = this.grid.instance.getRowIndexByKey(e.data.id);
  }

  onToolbarPreparingParent(e) {   
    e.toolbarOptions.items.unshift({
      location: 'after',
      widget: 'dxSelectBox',
      options: {
        label: "Filter: ",
        items: this.statuses,
        value: this.statuses[0],
        onValueChanged: (e) => {
          if (e.value == 'All') {
            this.grid.instance.clearFilter();
          } else {
            this.grid.instance.filter(['quoteStatusDisplay', '=', e.value]);
          }
        }
      }
    });
    e.toolbarOptions.items.unshift({
      location: 'after',
      widget: 'dxButton',
      options: {
        icon: 'plus',
        text: 'Add Quotation',
        type: "default",
        useSubmitBehavior: true,
        onClick: () => {
          this.action = 1;
          this.router.navigate([`/quotation-modify`], { state: { id: null, action: this.action } });
        }
      }
    });
  }

/* -------------- */

/* Internal Order Items*/
  onSavedChild(cellInfo) {
    cellInfo.setValue(cellInfo.value);
  }

  onInitNewRowChild(e) {
    e.data.id = 0;
    e.data.discount = 0;
  }

  onEditorPreparingChild(e) {
    if (e.parentType === "dataRow") {
      switch (e.dataField) {
        case "productId": {
          e.editorOptions.valueExpr = undefined;
          e.editorOptions.onFocusIn = function (args) {
            args.component.open();
          }
          e.editorOptions.onOpened = e2 => {
            e2.component._popup.option('width', 400);
            e2.component._popup.on(args => {
              args.component.option('width', 400);
            });
            e2.component._popup.off(args => {
              args.component.option('width', 400);
            });
          };
          break;
        }
        case "width":
        case "length":
        case "price":
        case "quatinty": {
          e.editorOptions.onOpened = e2 => {
            e2.component._popup.option('width', 400);
            e2.component._popup.on(args => {
              args.component.option('width', 400);
            });
            e2.component._popup.off(args => {
              args.component.option('width', 400);
            });
          };
          break;
        }
      }
    }
  }

  onToolbarPreparing(e): void {
    e.toolbarOptions.items.unshift({
      location: 'after',
      widget: 'dxButton',
      options: {
        icon: 'info',
        useSubmitBehavior: false,
        onClick: () => {
          this.popupVisible = true;
        }
      }
    });
  }

  onKeyDown(e) {
    if (e.event.key === "Tab" && this.savecolumn === true) {
      this.grid.instance.addRow();
    }

    if (e.event.key === "Enter" && this.savecolumn === true) {
      this.grid.instance.saveEditData();
    }

  }

  onFocusedCellChanged(e) {
    if (e.columnIndex === 7) {
      this.savecolumn = true;
    } else {
      this.savecolumn = false;
    }
  }

/* ------------------- */

/* Set Cell Values */
  setCellValueDeliveryCost(newData, value, currentRowData) {
    newData.deliveryCost = value;
    var total = 0;
    if (currentRowData.productData) {
      for (var i = 0; i < currentRowData.productData.length; i++) {
        total += currentRowData.productData[i].total;
      }
    }
  }

  setCellValueItemPrice(newData, value, currentRowData) {
    newData.price = value.price;
    newData.productId = value.id;
    //newData.packQuantity = value.packQuantity;

    if (currentRowData.quantity) {
      //newData.total = value.priceLookUpPrice * currentRowData.quantity;
      newData.total = value.price * currentRowData.quantity;
    } else {
      newData.total = 0;
    }
  }

  setTotal(newData, value, currentRowData) {
    newData.quantity = value;

    if (currentRowData.itemPrice != null) {
      newData.total = currentRowData.itemPrice * value;
    } else {
      newData.total = 0;
    }
  }

  setCoil(newData, value, currentRowData) {
    newData.length = value;
    if (currentRowData.width != null) {
      newData.coilTotal = currentRowData.width * value;
    }
  }

  setCoilPrice(newData, value, currentRowData) {
    newData.coilPrice = value
    if (currentRowData.coilTotal) {
      newData.itemPrice = currentRowData.coilTotal * value;
    }

  }

  setTransportTotal(newData, value, currentRowData) {
    newData.quantity = value;

    if (currentRowData.price != null) {
      newData.total = currentRowData.price * value;
    } else {
      newData.total = 0;
    }
  }
  /* --------------- */

  calculateTotal() {
    let total = 0;
    let vat = 0;
    this.data.quotationData.forEach(a => total += a.total);
    this.data.transportData.forEach(a => total += a.total);
    this.data.quotationData.forEach(a => { { vat += a.total * (this.vatperc / 100) } });

    this.data.total = total;
    this.data.vat = vat;
    this.data.totalInclVat = total + vat;
  }

  onHidden(e) {
    //reload internal orders
    this.dataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/quotation'
      })
    }
            /////////
  }
  getProduct(rowData) {
    return rowData.productCode + " - " + rowData.productName;
  }
/* --------------- */
}
