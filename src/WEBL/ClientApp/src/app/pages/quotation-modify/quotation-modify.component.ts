import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DxDataGridComponent, DxFormComponent, DxTooltipComponent } from 'devextreme-angular';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import { on } from 'devextreme/events';
import notify from 'devextreme/ui/notify';
import { QuotationService } from 'src/app/core/services/quotation.service';
import { currencyZAR } from 'src/globalConstants';
import { quote } from '../../core/models/quote.models';
import { quoteItems } from '../../core/models/quoteItems.models';
import { quoteTransports } from '../../core/models/quoteTransports.models';
import { quoteRevision } from '../../core/models/quoteRevision.models';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { OrdersService } from '../../core/services/orders.service';
import { StockService } from '../../core/services/stock.service';
import { tab } from "../models/tab";

@Component({
  selector: 'app-quotation-modify',
  templateUrl: './quotation-modify.component.html',
  styleUrls: ['./quotation-modify.component.scss']
})
export class QuotationModifyComponent implements OnInit{
  @ViewChild(DxTooltipComponent, { static: false }) meToolTip: DxTooltipComponent;
  UsersForeignDataSource: any;
  DepartmentManagersForeignDataSource: any;
  SupplierForeignDataSource: any;
  PlantLocationForeignDataSource: any;
  DepartmentForeignDataSource: any;
  StockForeignDataSource: any;
  GLCodeForeignDataSource: any;
  UnitofMeasureForeignDataSource: any;  
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  pageName: string = "Quotation Modify";
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
  popupVisible = false;
  popupVisibleP = false;
  popupVisibleD = false;
  popupVisibleS = false;
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
  newState = true;
  coilTotal: number;

  customerId = null;
  tempSupplierId = null;
  currency = "ZAR";
  isDisabled = false;
  tabs: tab[];
  popupClearItems = false;
  vatperc: any;
  toolTipText = "";
  //preview
  customer = null;
  requestedBy = null;
  placedBy = null;
  currentSupplier = "";
  oldSupplier = "";
  isEventRunning = false;
  currentSupplierValue = "";
  previousSupplierValue = "";
  eventofCombo: any;
  showServices = false;
  now: Date = new Date();
  transport = false;
  ProductDropdown: any;
  ProductForeignDataSource: any;
  CustomerForeignDataSource: any;
  QuotationForeignDataSource: any;
  revisionNumber: number = 1;
  statuses: string[];
  rowId: any;
  rowTransportId: any;
  quoteItemsPrice: number ;
  quoteItemsQuantity: number ;
  quoteItemsWidth: number ;
  quoteItemsLength: number ;
  quoteItemsCoil: number ;
  quoteItemsCoilPrice: number;
  quoteTransportPrice: number;
  quoteTransportQuantity: number;

  AddUpdateQuotationBtn: any = {
    text: "Submit",
    type: "success",
    useSubmitBehavior: true,
    validationGroup:"quotationData",
    onClick: (e) => {
      const res = e.validationGroup.validate();
      if (res.isValid) {
        if (this.itemGrid != undefined) {
          this.itemGrid.instance.saveEditData();
          this.itemGrid.instance.clearSelection();
          this.itemGrid.instance.option("focusedRowIndex", -1);

        }

        if (this.itemGrid.instance.getVisibleRows().length <= 0) {
          notify("The table should have a minimum of one item.", 'error', 5000);
          e.cancel = true;
          return;
        }

        this.data.quoteItems.forEach(x => {
          if (this.rowId == x.id) {
            if (x.coilPrice <= 0 || x.coilPrice != 0) {
              this.quoteItemsPrice = x.coilPrice;
            }
            if (x.quantity <= 0 || x.quantity != 0) {
              this.quoteItemsQuantity = x.quantity;
            }
            if (x.width <= 0 || x.width != 0) {
              this.quoteItemsWidth = x.width;
            }
            if (x.length <= 0 || x.length != 0) {
              this.quoteItemsLength = x.length;
            }
            if (x.coilTotal <= 0 || x.coilTotal != 0) {
              this.quoteItemsCoil = x.coilTotal;
            }
            if (x.price <= 0 || x.price != 0) {
              this.quoteItemsCoilPrice = x.price;
            }
          }
        })

        if (this.quoteItemsPrice <= 0 || this.quoteItemsQuantity <= 0 || this.quoteItemsCoilPrice <= 0) {
          notify("Quantity Or Price/m2 should be greater than 0", 'error', 5000);
          e.preventDefault();
          e.cancel = true;
          return
        }

        if (this.quoteItemsWidth <= 0 || this.quoteItemsLength <= 0) {
          notify("Width Or Length should be greater than 0", 'error', 5000);
          e.preventDefault();
          e.cancel = true;
          return
        }

        if (this.quoteItemsCoil <= 0 ) {
          notify("m2/Coil should be greater than 0", 'error', 5000);
          e.preventDefault();
          e.cancel = true;
          return
        }

        if (this.transport == true) {
          if (this.transGrid.instance.getVisibleRows().length <= 0) {
            notify("The tables should have a minimum of one item.", 'error', 5000);
            e.cancel = true;
            return;
          }

          if (this.transGrid.instance.hasEditData() == true) {
            notify("You have unsaved items.", 'error', 5000);
            e.cancel = true;
            return;
          }

          this.data.quoteTransports.forEach(t => {
            if (this.rowTransportId == t.id) {
              if (t.price <= 0 || t.price != 0) {
                this.quoteTransportPrice = t.price;
              }
              if (t.quantity <= 0 || t.quantity != 0) {
                this.quoteTransportQuantity = t.quantity;
              }
            }
          })

          if (this.quoteTransportPrice <= 0 || this.quoteTransportQuantity<= 0) {
              notify("Quantity Or Unit Price should be greater than 0", 'error', 5000);
              e.preventDefault();
              e.cancel = true;
              return
          }
        }

        if (this.itemGrid.instance.hasEditData() == true) {
          notify("You have unsaved items.", 'error', 5000);
          e.cancel = true;
          return;
        }

        if (this.data.customerId == null) {
          e.cancel = true;
          notify("Customer was not selected.", 'error', 5000);
          return
        }

        if (this.data.total <= 0) {
          e.cancel = true;
          notify("Total should be greater than 0", 'error', 5000);
          e.preventDefault();
          return
        }

        if (this.data.validFor == null || this.data.daysFrom == null || this.data.onOrder == null || this.data.onDelivery == null) {
          notify("Some fields are not filled in.", 'error', 5000);
          e.cancel = true;
          return;
        }

        if (this.data.validFor <= 0 || this.data.daysFrom <= 0 || this.data.onOrder <= 0 || this.data.onDelivery <= 0) {
          notify("Valid for OR Days from, should not be equal to 0.", 'error', 5000);
          e.cancel = true;
          return;
        }
          this.AddUpdateQuotation();
      }
        
    }
  }

  AddUpdateDraftQuotationBtn: any = {
    text: "Save Draft",
    type: "default",
    useSubmitBehavior: false,
    validationGroup: "quotationData",
    onClick: (e) => {
      const res = e.validationGroup.validate();
      if (res.isValid) {
        if (this.data.customerId == null) {
          e.cancel = true;
          notify("Customer was not selected.", 'error', 5000);
          return
        }

        this.AddUpdateDraftQuotation();
      }
    }
  }
    requestById: any;
    productName: any;
  
  OnCellPrepared(e) {
    if (e.rowType === "data" && (e.column.index === 0 || e.column.index === 1|| e.column.index === 2 || e.column.index === 3||e.column.index === 4)&&e.text!="") {
      on(e.cellElement, "mouseover", arg => {
        this.toolTipText = e.text;
        this.meToolTip.instance.show(arg.target);
      });

      on(e.cellElement, "mouseout", arg => {
        this.meToolTip.instance.hide();
      });
    }

    if (e.rowType === "data" && e.column.index == 6) {

      on(e.cellElement, "mouseout", arg => {
        this.meToolTip.instance.hide();
      });
    }
  }

  AddUpdateQuotationCancelBtn: any = {
    text: "Cancel",
    type: "default",
    useSubmitBehavior: true,
    onClick: () => {
      this.data = "";
      this.router.navigate([`/quotation`], { relativeTo: this.route }); 
    }
  }

  yesRemoveItemsOptions: any = {
    text: "Yes",
    onClick: (e) => {
      if (!this.transport ) {
        this.transport = this.eventofCombo.value;
        this.popupClearItems = false;
        var rows = this.transGrid.instance.getVisibleRows().length;
        let timeIndex = 0;
        for (let i = 0; i < rows; i++) {
          setTimeout(() => {
            this.transGrid.editing.confirmDelete = false;
            this.transGrid.instance.deleteRow(0);
            this.transGrid.editing.confirmDelete = true;


          }, 2000 * i);

          timeIndex = (i + 1) * 2100;
        }
        setTimeout(() => {
        }, timeIndex);
      }
    }
  }
  noRemoveItemsOptions: any = {
    text: "No",
    onClick: (e) => {
      this.isEventRunning = true;

      if (!this.transport) {
        this.transport = this.eventofCombo.previousValue;
        this.eventofCombo.component.option('value', this.eventofCombo.previousValue);
      }
      this.popupClearItems = false;
      
    }
  }

  @ViewChild('content') content;
  @ViewChild("products", { static: false }) itemGrid: DxDataGridComponent;
  @ViewChild("trans", { static: false }) transGrid: DxDataGridComponent;
  @ViewChild("mainform", { static: false }) mainForm: DxFormComponent;
  @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;
  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private stockService: StockService, private globalMethodsService: GlobalMethodsService, private orderService: OrdersService, private router: Router, private route: ActivatedRoute, private authService: AuthenticationService, private quotationService: QuotationService) {
    if (router.getCurrentNavigation() == null) {
      this.id = JSON.parse(sessionStorage.getItem('id')!);
      this.action = JSON.parse(sessionStorage.getItem('action')!);

    } else {
      this.action = router.getCurrentNavigation().extras.state.action;
      this.id = router.getCurrentNavigation().extras.state.id;
      sessionStorage.setItem("id", JSON.stringify(this.id));
      sessionStorage.setItem("action", JSON.stringify(this.action));
    }

    this.GetData();
    this.currencyZAR = currencyZAR;
    this.allowEdit = true;
    this.allowRedirect = false;
    this.hasComment = false;
    this.rowIndex = 0;
    this.heading = "Quotation";
    this.baseUrl = baseUrl;
    this.setCustomer = this.setCustomer.bind(this);
    this.setRequested = this.setRequested.bind(this);
    this.setPlaced = this.setPlaced.bind(this);
    this.setOrder = this.setOrder.bind(this);
    this.setDelivery = this.setDelivery.bind(this);

    this.statuses = ['All','Completed','Draft']; 

    this.QuotationForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Quotation'
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

    //For Requested by and Placed by
    this.UsersForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/FullEmployeeName',
      }),
      sort: [
        { selector: "FullName", desc: false }
      ],
      paginate: true
    }

    this.allowModify = this.authService.HavePermission(109);
  }

  handleTransport(e) {

    this.transGrid.instance.cancelEditData();

    if ((this.transGrid.instance.getVisibleRows().length > 0)) {
      if (!this.isEventRunning) {
        this.eventofCombo = e;
        this.popupClearItems = true;
      } else {
        this.isEventRunning = false;
      }
    }
    this.transport = e.value;
  }

  GetData() {
    this.stockService.GetVat().subscribe(data => {
      if (data != null) {
        this.vatperc = data;
      }
    });
    switch (this.action) {
      case 1: this.actionDisplay = "Add Quotation";              
        this.newState = true;
        this.authService.currentUserSubject.subscribe(user => {
          if (user) {
            this.data = new quote();
            this.data.placedById = user.id;
            this.data.requestById = user.id;
            this.data.customerId = this.customerId;
            this.data.total = 0;
            this.data.vat = 0;
            this.data.totalInclVat = 0;
            this.data.quoteItems = Array<quoteItems>();
            this.data.quoteTransports = Array<quoteTransports>();
            this.data.quoteRevision = Array<quoteRevision>();
          }
        });
        break;
      case 2: this.actionDisplay = "Update Quotation #" + this.id;
        this.newState = false;
        this.quotationService.GetQuotationsData(this.id, this.action).subscribe(data => {
          this.data = data;

          this.data.quoteItems.forEach(quoteItem => {

            quoteItem.coilTotal = quoteItem.width * quoteItem.length;
            if (quoteItem.coilTotal != 0) {
              quoteItem.coilPrice = quoteItem.price / quoteItem.coilTotal;
            }
            else {
              quoteItem.coilPrice = 0;
            }
            
            this.setTotal(quoteItem,quoteItem.quantity,quoteItem);
          });

         
          //preview building
          this.requestedBy = this.data.requestByFullName;
          this.placedBy = this.data.placedByFullName;
          this.customer = this.data.companyName;
          this.productName = this.data.productName;
          this.revisionNumber = this.data.quoteRevisions[0].revisionNr;

          if (this.data.quoteTransports.length > 0) {
            this.transport = true;
            this.data.quoteTransports.forEach(quoteTransports => {
              quoteTransports.total = quoteTransports.price * quoteTransports.quantity;
              this.setTransportTotalByQty(quoteTransports, quoteTransports.quantity, quoteTransports);
            });
          } 

          this.calculateTotal();
        }, subError => {
          notify(subError.error, 'error', 5000);
        });
        break;
    }
  }

  AddUpdateQuotation() {
    this.loadingVisible = true;
    this.quotationService.AddUpdateQuotations(this.data).subscribe(data => {
      this.router.navigate([`/quotation`], { relativeTo: this.route });
      this.loadingVisible = false;
      notify('Quotation Updated', 'success', 2000);      
    }, subError => {
      this.loadingVisible = false;
      notify(subError.error, 'error', 5000);
    });
  }

  AddUpdateDraftQuotation() {
    this.loadingVisible = true;
    this.quotationService.AddUpdateDraftQuotations(this.data).subscribe(data => {
      this.router.navigate([`/quotation`], { relativeTo: this.route });
      this.loadingVisible = false;
      notify('Quotation Draft Saved', 'success', 2000);
    }, subError => {
      this.loadingVisible = false;
      notify(subError.error, 'error', 5000);
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


  ngAfterContentInit() {
  }

  checkComparison() {
    return 0;
  }

  /* Internal Order */
  selectedChanged(e) {
    this.selectedRowIndex = e.component.getRowIndexByKey(e.selectedRowKeys[0]);
    this.allowEdit = true;
    this.allowRedirect = false;
    this.hasComment = false;
  }

  onEditorPreparingParent(e) {
    if (e.parentType === "dataRow") {
      switch (e.dataField) {
        case "requestedById":
        case "customerId": {
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
    this.router.navigateByUrl('/ ', { skipLocationChange: true }).then(() => {
        this.router.navigate(['/quotation'])   
    });
  }
  onEditCanceledParent(e) {
    this.router.navigateByUrl('/ ', { skipLocationChange: true }).then(() => {
      this.router.navigate(['/quotation'])
    });
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
        case "total": {
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
  onEditorPreparingTransport(e) {
    if (e.parentType === "dataRow") {
      switch (e.dataField) {
        case "description": {
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
        case "price":
        case "quantity": {
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
    newData.price = value.price;
    newData.productId = value.id;
    currentRowData.productId = value.id;
    newData.coilTotal = value.coilTotal;
    newData.coilPrice = value.coilPrice;
    newData.itemPrice = value.itemPrice;
    newData.length = value.length;
    newData.width = value.width;
  }

  setCellValueTransport(newData, value, currentRowData) {
    newData.price = value.price;
    newData.description = value.description;

    if (currentRowData.quantity) {
      newData.total = value.price * currentRowData.quantity;
    } else {
      newData.total = 0;
    }
  }

  setTotal(newData, value, currentRowData) {
    newData.quantity = value;

    if (value !=0) {
      newData.total = currentRowData.price * value;
    } else {
      newData.total = 0;
    }
  }

  setCoilWidth(newData, value, currentRowData) {
    newData.width = value;
    newData.coilTotal = 0;
    if (value != 0) {
      if (currentRowData.length) {
        newData.coilTotal = currentRowData.length * value;
      }
      else {
        newData.coilTotal = 0;
      }
    }
    else {
      newData.coilTotal = 0;
    }
  }

  setCoilLength(newData, value, currentRowData) {
    newData.length = value;
    newData.coilTotal = 0;
    if (value != 0) {
      if (currentRowData.width) {
        newData.coilTotal = currentRowData.width * value;
      }
      else {
        newData.coilTotal = 0;
      }
    }
    else {
      newData.coilTotal = 0;
    }
  }

  setCoilPrice(newData, value, currentRowData) {
    newData.coilPrice = value
    currentRowData.coilPrice = value

    if (newData.coilTotal) {
      newData.price = newData.coilTotal * value;
      newData.coilPrice = newData.price / newData.coilTotal;
    }
    else {
      newData.price = currentRowData.coilTotal * value;
    }
  }

  setCoilPriceByCoil(newData, value, currentRowData) {
    currentRowData.coilPrice = value
    if (currentRowData.coilTotal) {
      newData.price = currentRowData.coilTotal * value;
    }
    else {
      newData.price = currentRowData.coilTotal * value;
    }
  }

  setTransportTotalByQty(newData, value, currentRowData) {
    newData.quantity = value;

    if (value != 0) {
      newData.total = currentRowData.price * value;
    } else {
      newData.total = 0;
    }
  }

  setTransportTotalByPrice(newData, value, currentRowData) {
    newData.price = value;
    if (value != 0) {
      if (newData.quantity) {
        newData.total = newData.quantity * value;
      } else {
        newData.total = currentRowData.quantity * value;
      }
    }
    else {
      newData.total = 0;
    }
  }

  getTotalCoil(rowData) {
    if (rowData) {
      return rowData.coilTotal;
    } else {
      return 0;
    }
  }

  getCoilPrice(rowData) {
    let newPrice = 0;
    if (rowData.coilPrice != null) {
      newPrice = (rowData.width * rowData.length) * rowData.coilPrice;
      rowData.price = newPrice;
      return rowData.price;
    } else {
      return 0;
    }
    
  }
  getTotalPrice(rowData) {
    let price = 0;
    if (rowData.quantity != null) {
      price = (rowData.width * rowData.length) * rowData.coilPrice;
      rowData.total = price * rowData.quantity;
      return rowData.total;
    } else {
      return 0;
    }
  }

  getTotalTransport(rowData) {
    if (rowData.quantity != null) {
      return rowData.price * rowData.quantity;
    } else {
      return 0;
    }
  }

  setOrder(e) {
    if (e.value >= 0) {
      var o = 100 - e.value;
      this.data.onOrder = o;
    }
    else {
      notify("Number should be between 0-100", "error", 3000);
    }
  
  }

  setDelivery(e) {
    if (e.value >= 0) {
      var s = 100 - e.value;
      this.data.onDelivery = s;
    }
    else {
      notify("Number should be between 0-100", "error", 3000);
    }
  }
/* --------------- */

  calculateTotal() {
    let total = 0;
    let vat = 0;
    this.data.quoteItems.forEach(a => total += a.total);
    this.data.quoteTransports.forEach(a => total += a.total);
    this.data.quoteItems.forEach(a => { { vat += a.total * (this.vatperc / 100) } });
    this.data.quoteTransports.forEach(a => { { vat += a.total * (this.vatperc / 100) } });
  
    this.data.total = total;
    this.data.vat = vat;
    this.data.totalInclVat = total + vat;
  }

  onSaved(e) {
    this.calculateTotal();
  }

  onRowRemoved(e) {
    this.calculateTotal();
    //needs to change to quotation data
    if (this.data.quoteTransports.length == 0) {
      this.transport = false;
    }
  }

  setCustomer(e) {
   this.customerId = e.data.customerId;
   this.customer = e.component.option("text");
  }
  setRequested(e) {
    this.requestedBy = e.component.option("text");
  }

  setPlaced(e) {
    this.placedBy = e.component.option("text");
  }

  onHidden(e) {
    this.itemGrid.instance.getDataSource().reload();
  }

  editStart(e) {
    this.rowIndex = this.dataGrid.instance.getRowIndexByKey(e.data.id);
  }

  SavingStart(e) {
    this.rowIndex = 0;
  }

  insertRow(e) {
    this.id = e.data.id;
  }

  getProduct(rowData) {
    return rowData.productCode + " - " + rowData.productName;
  }

  startEdit(e) {
    if (e.rowType === "data") {
      e.component.editRow(e.data);
      this.rowId = e.data.id;

      this.data.quoteItems.forEach(x => {
        if (this.rowId == x.id) {
          if (x.coilPrice == 0 || x.coilPrice != 0) {
            this.quoteItemsPrice = x.coilPrice;
          }
          if (x.quantity == 0 || x.quantity != 0) {
            this.quoteItemsQuantity = x.quantity;
          }
          if (x.width == 0 || x.width != 0) {
            this.quoteItemsWidth = x.width;
          }
          if (x.length == 0 || x.length != 0) {
            this.quoteItemsLength = x.length;
          }
          if (x.coilTotal == 0 || x.coilTotal != 0) {
            this.quoteItemsCoil = x.coilTotal;
          }
          if (x.price == 0 || x.price != 0) {
            this.quoteItemsCoilPrice = x.price;
          }
        }
      })
    }
  }

  startEditTransport(e) {
    if (e.rowType === "data") {
      e.component.editRow(e.data);
      this.rowTransportId = e.data.id;

      this.data.quoteTransports.forEach(t => {
        if (this.rowTransportId == t.id) {
          if (t.price == 0 || t.price != 0) {
            this.quoteTransportPrice = t.price;
          }
          if (t.quantity == 0 || t.quantity != 0) {
            this.quoteTransportQuantity = t.quantity;
          }
        }
      })
    }
  }
}
