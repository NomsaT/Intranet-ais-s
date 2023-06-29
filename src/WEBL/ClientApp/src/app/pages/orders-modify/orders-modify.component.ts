import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DxDataGridComponent, DxFormComponent, DxTooltipComponent } from 'devextreme-angular';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import { on } from 'devextreme/events';
import notify from 'devextreme/ui/notify';

import { currencyZAR } from 'src/globalConstants';
import { internalOrder } from '../../core/models/internalOrder.models';
import { internalOrderItems } from '../../core/models/internalOrderItems.models';
import { onceOffItems } from '../../core/models/onceOffItems.models';
import { services } from '../../core/models/services.models';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { OrdersService } from '../../core/services/orders.service';
import { StockService } from '../../core/services/stock.service';
import { tab } from "../models/tab";

@Component({
  selector: 'app-orders-modify',
  templateUrl: './orders-modify.component.html',
  styleUrls: ['./orders-modify.component.scss']
})
export class OrdersModifyComponent implements OnInit{
  @ViewChild(DxTooltipComponent, { static: false }) meToolTip: DxTooltipComponent;
  UsersForeignDataSource: any;
  DepartmentManagersForeignDataSource: any;
  SupplierForeignDataSource: any;
  PlantLocationForeignDataSource: any;
  ProjectForeignDataSource: any;
  CompanyForeignDataSource: any;
  DepartmentForeignDataSource: any;
  StockForeignDataSource: any;
  GLCodeForeignDataSource: any;
  UnitofMeasureForeignDataSource: any;  
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  pageName: string = "Order";
  event: any;
  actionDisplay: any;
  action: any;
  id: any;
  data: any;
  currencyZAR: any;
  allowModify = false;
  allowProcessedModify = true;
  baseUrl: string;
  currentDeleteItem: any;
  popupDeleteItemtVisible: boolean = false;
  savecolumn = false;
  popupVisible = false;
  popupVisibleP = false;
  popupVisibleProj = false;
  popupVisibleComp = false;
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
  supplierId = null;
  tempSupplierId = null;
  currency = "ZAR";
  isDisabled = false;
  tabs: tab[];
  popupClearItems = false;
  popupClearLineItems = false;
  vatperc: any;
  toolTipText = "";
  currentDate = new Date();
  //preview
  plantLocation = null;
  project = null;
  company = null;
  supplierFullName = null;
  requestedBy = null;
  placedBy = null;
  approvedBy = null;
  currentSupplier = "";
  oldSupplier = "";
  isEventRunning = false;
  managersFullNames:any;
  currentSupplierValue = "";
  previousSupplierValue = "";
  eventofCombo: any;
  canAdd = false;
  showlistedItems = false;
  showOnceOffItems = false;
  showServices = false;
  statusDisplayLabel = "Loading...";
  link: any;
  defaultCompany: any;
  defaultCompanyValue: any;

  AddUpdateOrderBtn: any = {
    text: "Submit",
    type: "success",
    useSubmitBehavior: true,
    validationGroup:"internalOrderData",
    onClick: (e) => {
      var today = new Date();
      const res = e.validationGroup.validate();
      console.log(res);       
      if (res.isValid) {

        if (this.data.deliveryDate < this.data.followUpDate) {
          notify("The Follow Up Date cannot be after the Delivery Date.", 'error', 5000);
          e.cancel = true;
          return;
        }

        if (!this.data.companyId) {
          notify("Please select company", 'error', 5000);
          e.cancel = true;
          return;
        }

        if (this.data.deliveryDate < today || this.data.followUpDate < today ) {
          notify("The Follow Up Date OR Delivery Date cannot be prior to today.", 'error', 5000);
          e.cancel = true;
          return;
        }

        if (this.newState == false) {
          if (this.data.deliveryDate < today.toISOString().split('.')[0] || this.data.followUpDate < today.toISOString().split('.')[0]) {
            notify("The Follow Up Date OR Delivery Date cannot be prior to today.", 'error', 5000);
            e.cancel = true;
            return;
          }
        }

        if (this.itemGrid.instance.getVisibleRows().length <= 0 && this.onceGrid.instance.getVisibleRows().length <= 0 && this.serviceGrid.instance.getVisibleRows().length <= 0) {
          notify("The Internal Order should have a minimum of one line item.", 'error', 5000);
          e.cancel = true;
          return;
        }

        this.checkZeroQuantityValues(this.data.internalOrderItems, e);
        this.checkZeroQuantityValues(this.data.onceOffItems, e);
        this.checkZeroQuantityValues(this.data.services, e);

        if (this.itemGrid.instance.hasEditData() &&  this.showlistedItems == true) {
          notify("You have unsaved Listed items.", 'error', 5000);
          e.cancel = true;
          return;
        }

        if (this.onceGrid.instance.hasEditData() == true && this.showOnceOffItems == true) {
          notify("You have unsaved Once-Off items.", 'error', 5000);
          e.cancel = true;
          return;
        }

        if (this.serviceGrid.instance.hasEditData() == true && this.showServices == true) {
          notify("You have unsaved Services.", 'error', 5000);
          e.cancel = true;
          return;
        }

        this.AddUpdateInternalOrder();

      }
    }
  }

  AddUpdateDraftOrderBtn: any = {
    text: "Save Draft",
    type: "default",
    useSubmitBehavior: false,
    onClick: (e) => {
      if (this.itemGrid.instance.hasEditData() == true && this.showlistedItems == true) {
        notify("You have unsaved Listed items.", 'error', 5000);
        e.cancel = true;
        return;
      }

      if (this.onceGrid.instance.hasEditData() == true && this.showOnceOffItems==true) {
        notify("You have unsaved Once-Off items.", 'error', 5000);
        e.cancel = true;
        return;
      }

      if (this.serviceGrid.instance.hasEditData() == true && this.showServices == true) {
        notify("You have unsaved Services.", 'error', 5000);
        e.cancel = true;
        return;
      }

      this.AddUpdateDraftInternalOrder();
    }
  }

  AddUpdateDeleteDraftOrderBtn: any = {
    text: "Delete Draft",
    type: "danger",
    useSubmitBehavior: false,
    onClick: (e) => {
      this.AddUpdateDeleteDraftInternalOrder();
    }
  }

  checkZeroQuantityValues(items, e) {
    if (items.length > 0) {
      items.forEach(r => {
        if (r.quantity <= 0) {
          notify("Quantity should be greater than 0", 'error', 5000);
          e.preventDefault();
          e.cancel = true;
          return
        }
      })
    }
  }

  OnCellPrepared(e) {
    if (e.rowType === "data" &&
      (e.column.caption === "Internal Desc" || e.column.caption === "Supplier Desc" || e.column.caption === "GL Code" || e.column.caption === "Department" || e.column.caption === "UoM"
      || e.column.caption === "Item Code" || e.column.caption === "Code") && e.text != "") {
      on(e.cellElement, "mouseover", arg => {
        this.toolTipText = e.text;
        this.meToolTip.instance.show(arg.target);
      });

      on(e.cellElement, "mouseout", arg => {
        this.meToolTip.instance.hide();
      });
    }

    if (e.rowType === "data" && e.column.index == 6) {
    /*  on(e.cellElement, "mouseover", arg => {
        if (e.data.originalValue && e.data.packSize) {
          this.toolTipText = (e.data.originalValue / e.data.packSize).toFixed(2).toString() + "(" + this.currency + ")";
          console.log(this.toolTipText);
          this.meToolTip.instance.show(arg.target);
        }       
      });*/

      on(e.cellElement, "mouseout", arg => {
        this.meToolTip.instance.hide();
      });
    }
  }

  AddUpdateOrderCancelBtn: any = {
    text: "Cancel",
    type: "default",
    useSubmitBehavior: true,
    onClick: () => {
      this.data = "";
      this.router.navigate([`/orders`], { relativeTo: this.route }); 
    }
  }

  buttonOptionsP: any = {
    icon: "plus",
    type: "default",
    useSubmitBehavior: false,
    onClick: () => {
      this.popupVisibleP = true;
    }
  }

  buttonOptionsProj: any = {
    icon: "plus",
    type: "default",
    useSubmitBehavior: false,
    onClick: () => {
      this.popupVisibleProj = true;
    }
  }

  buttonOptionsComp: any = {
    icon: "plus",
    type: "default",
    useSubmitBehavior: false,
    onClick: () => {
      this.popupVisibleComp = true;
    }
  }

  buttonOptionsD: any = {
    icon: "plus",
    type: "default",
    useSubmitBehavior: false,
    onClick: () => {
      this.popupVisibleD = true;
    }
  }

  buttonOptionsS: any = {
    icon: "plus",
    type: "default",
    useSubmitBehavior: false,
    onClick: () => {
      this.popupVisibleS = true;
    }
  }

  yesRemoveItemsOptions: any = {
    text: "Yes",
    onClick: (e) => {
      this.supplierId = this.eventofCombo.value;
      this.popupClearItems = false;
      var rows = this.itemGrid.instance.getVisibleRows().length;
      //this.itemGrid.instance.clearFilter();
      let timeIndex = 0;
      this.loadingVisible = true;
      for (let i = 0; i < rows ; i++) {
        setTimeout(() => {
          this.itemGrid.editing.confirmDelete = false;
          this.itemGrid.instance.deleteRow(0);
          this.itemGrid.editing.confirmDelete = true;
      
          
        },2000*i);

        timeIndex = (i+1) * 2100;
      }
      setTimeout(() => {
        this.loadingVisible = false;
      }, timeIndex);
      
      
      
      this.StockForeignDataSource = {
        store: AspNetData.createStore({
          key: 'id',
          loadUrl: this.baseUrl + 'api/Stock',
        }),
        filter: ["supplierId", "=", this.supplierId],
        sort: [
          { selector: "InternalProductName", desc: false }
        ],
        paginate: true
      }

      this.stockService.GetCurrency(this.supplierId).subscribe(data => {
        if (data != null) {
          this.data.currency = data["iso"];
          this.currency = data["iso"];
        }
      }, subError => {
        notify(subError.error, 'error', 5000);
      });
     
    /*  while (this.newState) {
        this.itemGrid.instance.addRow();
      }*/
                  
      //this.popupDeleteItemtVisible = true;                 
      
    }
  }

  noRemoveItemsOptions: any = {
    text: "No",
    onClick: (e) => {
      //this.popupDeleteItemtVisible = true;
      this.isEventRunning = true;
      this.supplierId = this.eventofCombo.previousValue;
      this.eventofCombo.component.option('value', this.eventofCombo.previousValue);
      this.popupClearItems = false;
      
    }
  }

  yesRemoverLineItemsOptions: any = {
    text: "Yes",
    onClick: (e) => {
      if (!this.showlistedItems) {
        this.showlistedItems = this.eventofCombo.value;
        this.popupClearLineItems = false;
        var rows = this.itemGrid.instance.getVisibleRows().length;
        let timeIndex = 0;
        this.loadingVisible = true;
        for (let i = 0; i < rows; i++) {
          setTimeout(() => {
            this.itemGrid.editing.confirmDelete = false;
            this.itemGrid.instance.deleteRow(0);
            this.itemGrid.editing.confirmDelete = true;


          }, 2000 * i);

          timeIndex = (i + 1) * 2100;
        }
        setTimeout(() => {
        this.loadingVisible = false;
      }, timeIndex);
      }

     else if (!this.showOnceOffItems) {
        this.showOnceOffItems = this.eventofCombo.value;
        this.popupClearLineItems = false;
        var rows = this.onceGrid.instance.getVisibleRows().length;
        let timeIndex = 0;
        this.loadingVisible = true;
        for (let i = 0; i < rows; i++) {
          setTimeout(() => {
            this.onceGrid.editing.confirmDelete = false;
            this.onceGrid.instance.deleteRow(0);
            this.onceGrid.editing.confirmDelete = true;


          }, 2000 * i);

          timeIndex = (i + 1) * 2100;
        }
        setTimeout(() => {
          this.loadingVisible = false;
        }, timeIndex);
      }

      else if (!this.showServices) {
        this.showServices = this.eventofCombo.value;
        this.popupClearLineItems = false;
        var rows = this.serviceGrid.instance.getVisibleRows().length;
        let timeIndex = 0;
        this.loadingVisible = true;
        for (let i = 0; i < rows; i++) {
          setTimeout(() => {
            this.serviceGrid.editing.confirmDelete = false;
            this.serviceGrid.instance.deleteRow(0);
            this.serviceGrid.editing.confirmDelete = true;


          }, 2000 * i);

          timeIndex = (i + 1) * 2100;
        }
        setTimeout(() => {
          this.loadingVisible = false;
        }, timeIndex);
      }
    }
  }

  noRemoverLineItemsOptions: any = {
    text: "No",
    onClick: (e) => {
      this.isEventRunning = true;

      if (!this.showlistedItems) {
        this.showlistedItems = this.eventofCombo.previousValue;
        this.eventofCombo.component.option('value', this.eventofCombo.previousValue);
      }
      else if (!this.showOnceOffItems) {
        this.showOnceOffItems = this.eventofCombo.previousValue;
        this.eventofCombo.component.option('value', this.eventofCombo.previousValue);
      }
      else if (!this.showServices) {
        this.showServices = this.eventofCombo.previousValue;
        this.eventofCombo.component.option('value', this.eventofCombo.previousValue);
       
      }
       this.popupClearLineItems = false;

    }
  }
  @ViewChild('content') content;
  @ViewChild("ordersitems", { static: false }) itemGrid: DxDataGridComponent;
  @ViewChild("ordersonceitems", { static: false }) onceGrid: DxDataGridComponent;
  @ViewChild("ordersservices", { static: false }) serviceGrid: DxDataGridComponent;
  @ViewChild("mainform", { static: false }) mainForm: DxFormComponent;
  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private stockService: StockService, private globalMethodsService: GlobalMethodsService, private orderService: OrdersService, private router: Router, private route: ActivatedRoute, private authService: AuthenticationService) {
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
    this.heading = "Internal Order";
    this.baseUrl = baseUrl;
    this.setStockValue = this.setStockValue.bind(this);    
    this.setPlantLocation = this.setPlantLocation.bind(this);
    this.setProject = this.setProject.bind(this);
    this.setCompany = this.setCompany.bind(this);
    this.setRequested = this.setRequested.bind(this);
    this.setPlaced = this.setPlaced.bind(this);
    this.setApproved = this.setApproved.bind(this);

    this.setStockValue = this.setStockValue.bind(this);
    

    this.tabs = [
      { text: "Items" },

      { text: "Summary" }
    ];

    this.SupplierForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Supplier',
      }),
      sort: [
        { selector: "CompanyName", desc: false }
      ],
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
      sort: [
        { selector: "Name", desc: false }
      ],
      paginate: true
    }

    this.ProjectForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Project',
      }),
      sort: [
        { selector: "Name", desc: false }
      ],
      paginate: true
    }

    this.CompanyForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Company',
      }),
      sort: [
        { selector: "Name", desc: false }
      ],
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
      sort: [
        { selector: "Name", desc: false }
      ],
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
      sort: [
        { selector: "FullName", desc: false }
      ],
      paginate: true,
    }

    this.DepartmentManagersForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/FullEmployeeName',
      }),
      sort: [
        { selector: "FullName", desc: false }
      ],
      paginate: true,
    };

    this.StockForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Stock',
      }),
      sort: [
        { selector: "InternalProductName", desc: false }
      ],
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
      sort: [
        { selector: "Name", desc: false }
      ],
      paginate: true
    } 

    this.allowModify = this.authService.HavePermission(59);
  }
  //checkboxes
  handleListedItems(e) {
    this.itemGrid.instance.cancelEditData();      

    if ((this.itemGrid.instance.getVisibleRows().length > 0)) {
      if (!this.isEventRunning) {
        this.eventofCombo = e;
        this.popupClearLineItems = true;
      } else {
        this.isEventRunning = false;
      }
    }
    this.itemGrid.instance.addRow();
    this.showlistedItems = e.value;
  }

  handleOnceOffItems(e) {
    this.onceGrid.instance.cancelEditData();

    if ((this.onceGrid.instance.getVisibleRows().length > 0)) {
      if (!this.isEventRunning) {
        this.eventofCombo = e;
        this.popupClearLineItems = true;
      } else {
        this.isEventRunning = false;
      }
    }
    this.onceGrid.instance.addRow();
    this.showOnceOffItems = e.value;
  }

  handleServices(e) {
    this.serviceGrid.instance.cancelEditData();

    if ((this.serviceGrid.instance.getVisibleRows().length > 0)) {
      if (!this.isEventRunning) {
        this.eventofCombo = e;
        this.popupClearLineItems = true;
      } else {
        this.isEventRunning = false;
      }
    }
    this.serviceGrid.instance.addRow();
    this.showServices = e.value;
  }

  GetData() {
    this.stockService.GetVat().subscribe(data => {
      if (data != null) {
        this.vatperc = data;
      }
    });
    switch (this.action) {
      case 1: this.actionDisplay = "Add Internal Order";              
        this.newState = true;
        this.statusDisplayLabel = "New";

        this.orderService.GetDefaultCompany().subscribe(data => {
          if (data != null) {
            this.defaultCompany = data;
            this.authService.currentUserSubject.subscribe(user => {
              if (user) {
                this.data = new internalOrder();
                this.data.placedById = user.id;
                this.data.requestedById = user.id;
                this.data.total = 0;
                this.data.vat = 0;
                this.data.totalinclvat = 0;
                this.data.internalOrderItems = Array<internalOrderItems>();
                this.data.onceOffItems = Array<onceOffItems>();
                this.data.services = Array<services>();
                this.data.companyId = this.defaultCompany;
                this.defaultCompanyValue = this.defaultCompany;// sets a default value for the dx-select-box item
              }
            });
          }
        });
        
        break;
      case 2: this.actionDisplay = "Update Internal Order #" + this.id;
        this.newState = false;
        this.orderService.GetInternalOrderData(this.id, this.action).subscribe(data => {
          this.data = data;
          this.statusDisplayLabel = this.data.statusDisplay;
          this.defaultCompanyValue = this.data.companyId
          if (this.data.statusDisplay == "Close" || this.data.statusDisplay == "Approved") {
            this.allowProcessedModify = false;
            this.canAdd = false;
          }

          if (this.data.internalComment != null) {
            this.hasComment = true;
          }

          if (this.data.supplierId == null) {
            this.canAdd = false;
          } else {
            this.canAdd = true;
          }

          //checkboxes

          if (this.data.internalOrderItems.length > 0 ) {
            this.showlistedItems = true;

          }
          else {
            this.showlistedItems = false;

          }
 
          if (this.data.onceOffItems.length > 0) {
            this.showOnceOffItems = true;
          } else {
            this.showOnceOffItems = false;
          }
          if (this.data.services.length > 0) {
            this.showServices = true;
          } else {
            this.showServices = false;
          }
          

          //preview building
          this.requestedBy = this.data.requestByFullName;
          this.approvedBy = this.data.approveByFullName;
          this.placedBy = this.data.placedByFullName;
          this.plantLocation = this.data.plantLocationName;
          this.supplierFullName = this.data.supplierFullName;
          this.project = this.data.projectName;

          this.calculateTotal();

          this.stockService.GetCurrency(this.data.supplierId).subscribe(data => {
            if (data != null) {
              this.data.currency = data["iso"];
              this.currency = data["iso"];
            }
          });

        }, subError => {
          notify(subError.error, 'error', 5000);
        });
        break;
    }

    if (this.newState) {
      this.orderService.getDefaultDepartment().subscribe(data => {
        if (data != null) {
          this.data.departmentId = data;
        }
      }, subError => {
        notify(subError.error, 'error', 2000);
      });     
    }
  }

  AddUpdateInternalOrder() {
    console.log(this.authService.currentUser().username);
    let username = this.authService.currentUser().username;
    Object.assign(this.data, { username: username });

    this.loadingVisible = true;
    this.orderService.AddUpdateInternalOrder(this.data).subscribe(data => {
      this.router.navigate([`/orders`], { relativeTo: this.route });
      this.loadingVisible = false;
      notify('Internal Order Updated', 'success', 2000);      
    }, subError => {
      this.loadingVisible = false;
      notify(subError.error, 'error', 5000);
    });
  }

  AddUpdateDraftInternalOrder() {
    this.loadingVisible = true;
    this.orderService.AddUpdateDraftInternalOrder(this.data).subscribe(data => {
      this.router.navigate([`/orders`], { relativeTo: this.route });
      this.loadingVisible = false;
      notify('Internal Order Draft Saved', 'success', 2000);
    }, subError => {
      this.loadingVisible = false;
      notify(subError.error, 'error', 5000);
    });
  }

  AddUpdateDeleteDraftInternalOrder() {
    this.orderService.AddUpdateDeleteDraftInternalOrder(this.data.id).subscribe(data => {
      this.router.navigate([`/orders`], { relativeTo: this.route });
      notify('Internal Order Draft Deleted', 'success', 2000);
    }, subError => {
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

  onEditorPreparingParent(e) {
    if (e.parentType === "dataRow") {
      switch (e.dataField) {
        case "requestedById":
        case "supplierId":
        case "plantLocationId":
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
    this.router.navigateByUrl('/ ', { skipLocationChange: true }).then(() => {
        this.router.navigate(['/orders'])   
    });
  }
  onEditCanceledParent(e) {
    this.router.navigateByUrl('/ ', { skipLocationChange: true }).then(() => {
        this.router.navigate(['/orders'])
    });
  }

  onEditingStartParent(e) {
    this.heading = "Internal Order #" + e.data.id;

  
  }

  onInitNewRowListedItems(e) {
    e.data.id = 0;
    e.data.vatAppl = true;
    e.data.grnAppl = true;

    this.StockForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/Stock',
      }),
      filter: ["supplierId", "=", this.data.supplierId],
      sort: [
        { selector: "InternalProductName", desc: false }
      ],
      paginate: true
    }
  }

  onInitNewRowNotListedItems(e) {
    e.data.id = 0;
    e.data.vatAppl = true;
    e.data.grnAppl = false;
  }

  onInitNewRowServiceItems(e) {
    e.data.id = 0;
    e.data.vatAppl = true;
    e.data.grnAppl = false;
  }

  onEditorPreparingChild(e) {
    if (e.parentType === "dataRow") {
      switch (e.dataField) {
        case "stockId": {
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
        case "glcodeId":
        case "departmentId":
        case "uomid": {
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

  onEditorPreparingTemp(e) {
    if (e.parentType === "dataRow") {
      switch (e.dataField) {
        case "glcodeId":
        case "departmentId":
        case "uomid": {
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

  onEditorPreparingService(e) {
    if (e.parentType === "dataRow") {
      switch (e.dataField) {
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
  /*  newData.value = value.currentPrice;
    newData.originalValue = value.currentPrice;*/
    newData.value = value.itemPrice;
    newData.originalValue = value.itemPrice;
    newData.stockId = value.id;
    newData.packSize = value.packSize;
    newData.uomid = value.uomid;
    newData.uomName = value.uomName;
    newData.itemPrice = value.itemPrice;
    newData.departmentId = value.defaultDepartmentId;
    newData.uomPrice = value.currentPrice;
    //newData.packQuantity = value.packQuantity;

    if (currentRowData.quantity) {
      //newData.total = value.priceLookUpPrice * currentRowData.quantity;
      newData.total = value.itemPrice * currentRowData.quantity;
    } else {
      newData.total = 0;
    }
  }

  setCellValueQuantity(newData, value, currentRowData) {
    newData.quantity = value;
    if (currentRowData.value) {
      newData.total = currentRowData.value * value;
   /*   newData.total = currentRowData.itemPrice * value;*/
    } else {
      newData.total = 0;
    }
    if (currentRowData.packSize) {
      newData.totalUnits = currentRowData.packSize * newData.quantity
    } else {
      newData.totalUnits = 0;
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

  setCellValuePrice(newData, value, currentRowData) {
    newData.value = value;
    if (currentRowData.quantity) {
      //newData.total = value * currentRowData.quantity;
      if (value == 0) {
        newData.total = currentRowData.originalValue * currentRowData.packSize * currentRowData.quantity;
      } else {
        newData.total = value * currentRowData.packSize * currentRowData.quantity;
      }
    } else {
      newData.total = 0;
    }
  }

  //services
  setServiceCellValuePrice(newData, value, currentRowData) {
    newData.value = value;
    //newData.total = newData.value;
    if (currentRowData.quantity) {
      newData.total = currentRowData.quantity * value;
    }
    else {
      newData.total = value;
    }
  }

  setServiceCellQty(newData, value, currentRowData) {
    newData.quantity = value;
    if (currentRowData.value) {
      //newData.total = currentRowData.value * value;
      newData.total = currentRowData.value * value;
    } else {
      newData.total = value;
    }
  }

  //Once off items
  setCellValueOnceQuantity(newData, value, currentRowData) {
    newData.quantity = value;
    if (currentRowData.value) {
      newData.total = currentRowData.value * value;
    } else {
      newData.total = 0;
    }
    if (currentRowData.packSize) {
      newData.totalUnits = currentRowData.packSize * newData.quantity
    } else {
      newData.totalUnits = 0;
    }
  }

  setCellOncePackSize(newData, value, currentRowData) {
    newData.packSize = value;
    if (currentRowData.quantity) {
      newData.totalUnits = currentRowData.quantity * newData.packSize
    } else {
      newData.totalUnits = 0;
    }
  }

  setCellValueOncePrice(newData, value, currentRowData) {
    newData.value = value;
    if (currentRowData.quantity) {
      newData.total = value * currentRowData.quantity;
      //newData.total = value * currentRowData.packQuantity * currentRowData.quantity;
    } else {
      newData.total = 0;
    }
  }
/* --------------- */

  calculateTotal() {
    let total = 0;
    let vat = 0;
    this.data.internalOrderItems.forEach(a => total += a.total);
    this.data.internalOrderItems.forEach(a => { if (a.vatAppl) { vat += a.total * (this.vatperc / 100) } });
    this.data.onceOffItems.forEach(a => total += a.total);
    this.data.onceOffItems.forEach(a => { if (a.vatAppl) { vat += a.total * (this.vatperc / 100) } });
    this.data.services.forEach(a => total += a.total);
    this.data.services.forEach(a => { if (a.vatAppl) { vat += a.total * (this.vatperc / 100) } });
  

    this.data.total = total;
    this.data.vat = vat;
    this.data.totalinclvat = total + vat;
  }

  onSaved(e) {
    this.calculateTotal();

  }

  onRowRemoved(e) {
    this.calculateTotal();
    if (this.data.internalOrderItems.length == 0) {
      this.showlistedItems = false;
    }
    if (this.data.onceOffItems.length == 0) {
      this.showOnceOffItems = false;
    } 
    if (this.data.services.length == 0) {
      this.showServices = false;
    }
  }

  setStockValue(e) {
    this.itemGrid.instance.cancelEditData();

    if ((this.itemGrid.instance.getVisibleRows().length > 0)) {
      if (!this.isEventRunning) {
        this.eventofCombo = e;
        this.popupClearItems = true;
      } else {
        this.isEventRunning = false;
      }
    }

    this.supplierId = e.value;
    this.supplierFullName = e.component.option("text");

    if (this.supplierId == null) {
      this.canAdd = false;
    } else {
      this.canAdd = true;
    }
   
    /*if (this.newState) {
      this.itemGrid.instance.addRow();
    }*/

    this.StockForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/Stock',
      }),
      filter: ["supplierId", "=", this.supplierId],
      sort: [
        { selector: "InternalProductName", desc: false }
      ],
      paginate: true
    }

  }

  setPlantLocation(e) {
    this.plantLocation = e.component.option("text");
  }

  setProject(e) {
    this.project = e.component.option("text");
  }

  setCompany(e) {
    this.company = e.component.option("text");
    this.data.companyId = e.value;
  }

  setRequested(e) {
    this.requestedBy = e.component.option("text");
  }

  setPlaced(e) {
    this.placedBy = e.component.option("text");
  }

  setApproved(e) {
    this.approvedBy = e.component.option("text");
  }

  onHidden(e) {
    this.itemGrid.instance.getDataSource().reload();
  }

  openPO(e) {
    var link = this.baseUrl + 'api/GetPDF?Id=' +this.data.id;
    window.open(link);
  }
}
