import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DxDataGridComponent } from 'devextreme-angular';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import notify from 'devextreme/ui/notify';
import { currencyZAR, Listed, OnceOff, Service } from 'src/globalConstants';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { OrdersService } from '../../core/services/orders.service';
import { StockService } from '../../core/services/stock.service';
import { tab } from "../models/tab";

@Component({
  selector: 'app-orders-report-view',
  templateUrl: './orders-report-view.component.html',
  styleUrls: ['./orders-report-view.component.scss']
})
export class OrdersReportViewComponent implements OnInit {
  UsersForeignDataSource: any;
  DepartmentManagersForeignDataSource: any;
  SupplierForeignDataSource: any;
  PlantLocationForeignDataSource: any;
  DepartmentForeignDataSource: any;
  StockForeignDataSource: any;
  StorageTypeForeignDataSource: any;
  GLCodeForeignDataSource: any;  
  UnitofMeasureForeignDataSource: any;
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  pageName: string = "Order View";
  event: any;
  actionDisplay: any;
  action: any;
  id: any;
  data: any;
  currencyZAR: any;
  allowModify = false;
  baseUrl: string;
  currentDeleteItem: any;
  popupDeleteItemtVisible: boolean = false;
  savecolumn = false;
  StorageTypeObj: any;
  userId: any;
  columnIndex: any;
  selectedRowIndex = -1;
  heading: any;
  defaultVisible = false;
  allowEdit = true;
  allowRedirect = false;
  orderId: any;
  internalComment: any;
  loadingVisible = false;
  hasComment = false;
  department: any;
  rowIndex = 0;
  newState:any;
  canEdit = false;
  showBtn = true;
  departmentId = null;
  isDisabled = false;
  supplierId = null;
  currency = "ZAR";
  tabs: tab[];
  popupClearItems = false;
  vatperc: any;

  AddUpdateOrderBtn: any = {
    text: "Submit",
    type: "success",
    useSubmitBehavior: true,
    validationGroup: "internalOrderData",
    onClick: (e) => {
      var today = new Date();
      const res = e.validationGroup.validate();
      console.log(res);
      if (res.isValid) {
        if (this.data.deliveryDate < this.data.followUpDate) {
          notify("The Follow Up Date cannot be after the Delivery Date.", 'error', 5000);
        } else if (this.data.deliveryDate < today || this.data.followUpDate < today) {
          notify("The Follow Up Date OR Delivery Date cannot be prior to today.", 'error', 5000);
        } else {
          this.AddUpdateInternalOrder();
        }
      }
    }
  }

  AddUpdateOrderCancelBtn: any = {
    text: "Cancel",
    type: "default",
    useSubmitBehavior: true,
    onClick: () => {
      this.data = "";
      this.router.navigate([`/approved-orders-report`], { relativeTo: this.route });
    }
  }

  @ViewChild('content') content;
  @ViewChild("ordersitems", { static: false }) itemGrid: DxDataGridComponent;
  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private stockService: StockService, private globalMethodsService: GlobalMethodsService, private orderService: OrdersService, private router: Router, private route: ActivatedRoute, private authService: AuthenticationService) {
    this.action = router.getCurrentNavigation().extras.state.action;
    this.id = router.getCurrentNavigation().extras.state.id;
    this.canEdit = router.getCurrentNavigation().extras.state.canEdit;
    this.GetData();
    this.currencyZAR = currencyZAR;
    this.allowEdit = true;
    this.allowRedirect = false;
    this.hasComment = false;
    this.rowIndex = 0;
    this.heading = "Internal Order";
    this.baseUrl = baseUrl;
    this.setStockValue = this.setStockValue.bind(this);

    this.tabs = [
      { text: "Listed Items" },
      { text: "Once-Off Items" },
      { text: "Services" },
      /* { text: "Reset Stock" }*/
    ];

    this.stockService.GetStorageType().subscribe(data => this.StorageTypeObj = data);

    this.SupplierForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Supplier',
      }),
      paginate: true
    }

    this.PlantLocationForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/PlantLocation',
        /* onBeforeSend: (method, param) => {
           param.headers = {
             'Authorization': 'Bearer ' + this.token
           };
         }*/
      }),
      paginate: true
    }

    this.DepartmentForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Department',
        /* onBeforeSend: (method, param) => {
           param.headers = {
             'Authorization': 'Bearer ' + this.token
           };
         }*/
      }),
      paginate: true
    }

    this.UsersForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/FullEmployeeName',
        /*onBeforeSend: (method, param) => {
          param.headers = {
            'Authorization': 'Bearer ' + this.token
          };
        }*/
      }),
      paginate: true
    }

    this.DepartmentManagersForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/FullEmployeeName',
        /*loadUrl: baseUrl + 'api/FullEmployeeName/getFilteredDepartmentManagers',*/
        /*onBeforeSend: (method, param) => {
          param.headers = {
            'Authorization': 'Bearer ' + this.token
          };
        }*/
      }),
      paginate: true
    }

    this.StockForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Stock',
      }),
      paginate: true
    }

    this.StorageTypeForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/StorageType',
      }),
      paginate: true
    }

    this.GLCodeForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/GLCodes',
      }),
      sort: [
        { selector: "Glfullname", desc: false }
      ],
      paginate: true
    }

    this.UnitofMeasureForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/UnitOfMeasurement',
      }),
      paginate: true
    }

    this.allowModify = this.authService.HavePermission(59);
  }

  GetData() {
    this.actionDisplay = "View Internal Order #" + this.id;
    
    this.orderService.GetInternalOrderData(this.id, this.action).subscribe(data => {
      this.data = data;
      this.data.totalinclvat = this.data.total + this.data.vat;
      this.data.totalUnits = this.data.quantity * this.data.packSize;

      this.calculateTotal();

      this.stockService.GetCurrency(this.data.supplierId).subscribe(data => {
        if (data != null) {
          this.data.currency = data["iso"];
          this.currency = data["iso"];
        }
      }, subError => {
        notify(subError.error, 'error', 5000);
      });
      this.newState = this.data.statusDisplay;
      if (this.data.statusDisplay == "Denied") {
        this.showBtn = false;
      }
    }, subError => {
      notify(subError.error, 'error', 5000);
    });
  }

  AddUpdateInternalOrder() {
    this.router.navigate([`/approved-orders-report`], { relativeTo: this.route });
    this.orderService.AddUpdateInternalOrder(this.data).subscribe(data => {
      notify('Internal Order Updated', 'success', 2000);
    }, subError => {
      notify(subError.error, 'error', 2000);
    });
  }

  ngOnInit() {
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

  ngAfterViewInit() {

  }

  checkComparison() {
    return 0;
  }

  getStock(rowData) {
    if (rowData) {
      return rowData.code + " - " + rowData.productName + " - " + rowData.internalProductName;
    } else {
      return "";
    }
  }

  getGLName(rowData) {
    if (rowData) {
      return rowData.code + " - " + rowData.name;
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
    this.hasComment = false;
    if (e.selectedRowsData[0].internalComment != null) {
      this.hasComment = true;
    }
  }

  onCellPrepared(e) {
    if (e.rowType === 'data') {
      e.cellElement.style.backgroundColor = "#ffffff";
    }
  }

  onEditorPreparingParent(e) {
    if (e.parentType === "dataRow") {
      switch (e.dataField) {
        case "requestedById":
        case "supplierId":
        case "plantLocationId":
        case "departmentId":
        case "approveById": {
          e.editorOptions.onFocusIn = function (args) {
            setTimeout(function () {
              if (!(args.component.option('opened'))) {
                args.component.open();
              }
            }, 200)
          }
          break;
        }
        case "followUpDate":
        case "deliveryDate": {
          e.editorOptions.onFocusIn = function (args) {
            setTimeout(function () {
              if (!(args.component.option('opened'))) {
                args.component.open();
              }
            }, 200)
          }
          e.editorOptions.invalidDateMessage = "The date must have the following format: yyyy/MM/dd";
          break;
        }
      }
    }
  }

  onSavedParent(e) {
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() =>
      this.router.navigate(['/approved-orders-report'])
    );
  }

  onEditCanceledParent(e) {
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() =>
      this.router.navigate(['/approved-orders-report'])
    );
  }

  onEditingStartParent(e) {
    this.heading = "Internal Order #" + e.data.id;
  }

  onEditorPreparingChild(e) {
    if (e.parentType === "dataRow") {
      switch (e.dataField) {
        case "stockId": {
          e.editorOptions.valueExpr = undefined;
          e.editorOptions.onFocusIn = function (args) {
            args.component.open();
          }
          break;
        }
        case "glcodeId": {
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

  onKeyDown(e) {

  }

  onFocusedCellChanged(e) {
    if (e.columnIndex === 7) {
      this.savecolumn = true;
    } else {
      this.savecolumn = false;
    }
  }

  setCellValueItemPrice(newData, value, currentRowData) {
    newData.value = value.currentPrice;
    newData.originalValue = value.currentPrice;
    newData.stockId = value.id;
    newData.packSize = value.packSize;
    newData.uomid = value.uomid;

    if (currentRowData.quantity) {
      newData.total = value.priceLookUpPrice * currentRowData.quantity;
    } else {
      newData.total = 0;
    }
  }

  setCellValueQuantity(newData, value, currentRowData) {
    newData.quantity = value;
    if (currentRowData.value) {
      newData.total = currentRowData.value * value;
    } else {
      newData.total = 0;
    }
    if (currentRowData.packSize) {
      newData.totalUnits = newData.quantity * currentRowData.packSize
    } else {
      newData.totalUnits = 0;
    }
  }

  setCellValuePrice(newData, value, currentRowData) {
    newData.value = value;
    if (currentRowData.quantity) {
      newData.total = value * currentRowData.quantity;
    } else {
      newData.total = 0;
    }
  }

  setCellPackSize(newData, value, currentRowData) {
    newData.packSize = value;
    if (currentRowData.quantity) {
      newData.totalUnits = currentRowData.quantity * newData.packSize
    } else {
      newData.totalUnits = 0;
    }
  }

  setServiceCellValuePrice(newData, value, currentRowData) {
    newData.value = value;
    newData.total = value;
  }

/*  setGrn(newData, value, currentRowData) {
    newData.itemTypeId = value;

    switch (newData.itemTypeId) {
      case Listed: {
        newData.grnAppl = true;
        newData.vatAppl = true;
        this.isDisabled = true
        break;
      }
      case OnceOff: {
        newData.grnAppl = false;
        newData.vatAppl = true;
        this.isDisabled = false
        break;
      }
      case Service: {
        newData.grnAppl = false;
        newData.vatAppl = true;
        this.isDisabled = false
        break;
      }
    }
  }*/

  setStockValue(e) {
    this.supplierId = e.value;
    if (this.newState) {
      this.itemGrid.instance.addRow();
    }

    this.StockForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/Stock',
      }),
      filter: ["supplierId", "=", this.supplierId],
      paginate: true
    }

    this.stockService.GetCurrency(e.value).subscribe(data => {
      if (data != null) {
        this.data.currency = data["iso"];
        this.currency = data["iso"];
      }
    }, subError => {
      notify(subError.error, 'error', 5000);
    });
  }
  /* --------------- */
  onFieldDataChanged(e) {
    if (e.dataField == "deliveryCost" || e.dataField == "discount") {
      this.calculateTotal();
    }
  }

  calculateTotal() {
    this.stockService.GetVat().subscribe(data => {
      this.vatperc = data;
    

    let total = 0;
    let vat = 0;
    this.data.internalOrderItems.forEach(a => total += a.total);
    this.data.internalOrderItems.forEach(a => { if (a.vatAppl) { vat += a.total * (this.vatperc / 100) } });
    this.data.onceOffItems.forEach(a => total += a.total);
    this.data.onceOffItems.forEach(a => { if (a.vatAppl) { vat += a.total * (this.vatperc / 100) } });
    this.data.services.forEach(a => total += a.value);
    this.data.services.forEach(a => { if (a.vatAppl) { vat += a.value * (this.vatperc / 100) } });

    this.data.total = total;
    this.data.vat = vat;
      this.data.totalinclvat = total + vat;
    })
  }

  onSaved(e) {
    this.calculateTotal();
  }

  onRowRemoved(e) {
    this.calculateTotal();
  }

  onHidden(e) {
    this.itemGrid.instance.getDataSource().reload();
  }
}
