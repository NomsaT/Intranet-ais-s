import { Component, Inject, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DxDataGridComponent, DxToolbarComponent, DxToolbarModule } from 'devextreme-angular';
import { DxoLookupComponent } from 'devextreme-angular/ui/nested';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import { exportDataGrid } from 'devextreme/excel_exporter';
import notify from 'devextreme/ui/notify';
import { Workbook } from 'exceljs';
import saveAs from 'file-saver';
import { currencyZAR, deniedStatus, pendingStatus, reviewStatus, pendingMonitoredStatus, draftStatus, closeStatus, approvedStatus } from 'src/globalConstants';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { StockService } from '../../core/services/stock.service';
import { OrdersService } from '../../core/services/orders.service';
import { PriceIncreaseService } from '../../core/services/priceIncrease.service';
import { PriceLookUpService } from '../../core/services/pricelookup.service';
import { priceIncrease } from '../../core/models/priceIncrease.models';
import { UserProfileService } from 'src/app/core/services/user.service';
import { formatDate } from '@angular/common';
import DataSource from 'devextreme/data/data_source';
import { start } from 'repl';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit, OnDestroy {
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
  pageName: string = "Order";
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

  //for filtering in the tool bar
  startDate = "";
  endDate = ""  ;
  toolbarItems: any = [];
  maxDate=new Date()

  managersFullNames: any = [];
  defaultVisible = false;
  allowEdit = true;
  allowRedirect = false;
  orderId: any;
  internalComment: any;
  loadingVisible = false;
  hasComment = false;
  department: any;
  rowIndex = 0;
  action: any;
  priceIncreases: any;
  popupquestion = false;

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
    text: "Edit Internal Order",
    onClick: (e) => {
      this.router.navigate(['/orders-modify'], { state: { id: this.orderId, action: this.action } });
      this.popupquestion = false;
    }
  }

  approveOption: any = {
    text: "Approve Price Increase",
    onClick: (e) => {
      this.popupquestion = false;
      this.orderService.getMonitoredPendingApproval(this.orderId).subscribe(data => {
        if (data != null) {
          this.priceIncreases = data;
          //open popup
          this.popupVisiblePriceIncrease = true;

        }
      }, subError => {
        notify(subError.error, 'error', 5000);
      });
    }
  }
  @ViewChild('content') content;
  @ViewChild("orders", { static: false }) grid: DxDataGridComponent;
  @ViewChild("OrderItem", { static: false }) datagrid: DxDataGridComponent;
  @ViewChild("stockId", { static: false }) lookup: DxoLookupComponent;
  @ViewChild(DxToolbarComponent, { static: false }) toolbar: DxToolbarComponent;
  username: string;
  constructor(@Inject('BASE_URL') baseUrl: string,
    private userService: UserProfileService, private pricelookupService: PriceLookUpService, private modalService: NgbModal, private orderService: OrdersService, private priceIncreaseService: PriceIncreaseService, private globalMethodsService: GlobalMethodsService, private authService: AuthenticationService, private stockService: StockService, private router: Router, private route: ActivatedRoute,) {
    this.baseUrl = baseUrl;
    this.currencyZAR = currencyZAR;
    this.allowEdit = true;
    this.allowRedirect = false;
    this.hasComment = false;
    this.rowIndex = 0;
    this.heading = "Internal Order";
    this.statuses = ['All', 'Open', 'Pending','Pending Price Approval', 'Review', 'Draft', 'Close']; 
    this.username = this.authService.currentUser().username;
    async function makeHttpRequest(url) {
      // ...
    }

    this.userService.getAllManagers().subscribe(managersList => {
      //this.managersFullNames=data;
      managersList.data.forEach(item => {
        this.managersFullNames.push(item.fullName);
      });
      this.managersFullNames.push("All");
      console.log(this.managersFullNames);
    });

    this.authService.currentUserSubject.subscribe(user => {
      if (user) {
        this.userId = user.id
      }
    })

    this.getIntialDataSource();

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

    this.DepartmentManagersForeignDataSource = {
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

    this.StockForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Stock',
      }),
      paginate: true
    }

    this.GLCodeForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/GLCodes',
      }),
      paginate: true
    }

    this.allowModify = this.authService.HavePermission(59);
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

  getIntialDataSource() {
    this.dataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/InternalOrder',
        updateUrl: this.baseUrl + 'api/InternalOrder',
        insertUrl: this.baseUrl + 'api/InternalOrder',
        deleteUrl: this.baseUrl + 'api/InternalOrder',
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
          param.headers = {
            'Authorization': 'Bearer ' + this.authService.getToken()
          };
        }*/
      }),
      paginate: true,
      sort: ([
        { selector: "name", desc: false }
      ])
    }
  }

  ngOnDestroy() {
    try {
      this.datagrid.instance.dispose();
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

  redirectRow() {
    this.router.navigate([`/approved-orders-report`], { queryParams: { orderId: this.orderId, internalComment: this.internalComment }, relativeTo: this.route });
    this.orderId = null;
    this.internalComment = null;
  }

  getFullName(rowData) {
    return rowData.code + " - " + rowData.companyName;
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

    if (e.selectedRowsData[0].statusId == deniedStatus) {
      this.allowEdit = false;
      this.orderId = e.selectedRowsData[0].id;
      this.internalComment = e.selectedRowsData[0].internalComment;
      if (this.orderId != null) {
        this.allowRedirect = true;
      }
    } else if (e.selectedRowsData[0].statusId == pendingStatus || e.selectedRowsData[0].statusId == reviewStatus || e.selectedRowsData[0].statusId == draftStatus || e.selectedRowsData[0].statusId == closeStatus || e.selectedRowsData[0].statusId == approvedStatus) {
      this.orderId = e.selectedRowsData[0].id;
      this.action = 2;
      this.router.navigate(['/orders-modify'], { state: { id: this.orderId, action: this.action } });
    } else if (e.selectedRowsData[0].statusId == pendingMonitoredStatus) {
      this.orderId = e.selectedRowsData[0].id;
      this.action = 2;

      this.popupquestion = true;

    }

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

  onInitNewRow(e) {
    e.data.placedById = this.userId;
    e.data.requestedById = this.userId;
    e.data.deliveryCost = 0;
    e.data.total = 0;
    e.data.discount = 0;
    e.data.internalOrderItems = [];
    this.heading = "New Internal Order"; 
  }

  onEditorPreparingParent(e) {
    if (e.parentType === "dataRow") {
      switch (e.dataField) {
        case "requestedById": 
        case "supplierId": 
        case "plantLocationId": 
        case "departmentId": 
        case "approveById":  {
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
      this.router.navigate(['/orders'])
    );
  }

  onEditCanceledParent(e) {
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() =>
      this.router.navigate(['/orders'])
    );
  }

  onSaving(e) {
    if (this.datagrid.instance.hasEditData() == true) {    
      e.cancel = true;
      notify("Make sure all Interal Order Items are saved.", 'error', 5000);
    } 

    if (this.datagrid.instance.getVisibleRows().length <= 0) {     
      e.cancel = true;
      notify("The Internal Order should have a minimum of one item.", 'error', 5000);
    }
    this.rowIndex = 0;
  }

  onEditingStartParent(e) {
    this.heading = "Internal Order #" + e.data.id;
    this.rowIndex = this.grid.instance.getRowIndexByKey(e.data.id);
  }

  onToolbarPreparingParent(e) {  
    
    e.toolbarOptions.items.unshift({
      location: 'after',
      widget: 'dxSelectBox',
      options: {
        label: "Filter: ",
        name: "Status",
        items: this.statuses,
        value: this.statuses[0],
        onValueChanged: (e) => {
          if (e.value != null) {
            var toolBarItemApproveBy = this.toolbarItems.find(item => item.options.name == 'ApproveBy');
            if ((this.startDate == "" && this.endDate == "") || toolBarItemApproveBy.options.value != null) {
              this.getIntialDataSource();
              toolBarItemApproveBy.options.value = null;
            }
            this.dataSource = {
              filter: ([
                ["!", "approveByFullName", "=", e.value]
              ])
            }

            if (e.value == 'All') {
              this.dataSource = {
                filter: ["!", ["statusDisplay", "=", e.value]]
              }
            } else {
              if (e.value == "Open") {
                this.dataSource = {
                  filter: (["statusDisplay", "=", "Approved"])
                }
              } else {
                this.dataSource = {
                  filter: (["statusDisplay", "=", e.value])
                }
              }
            }
          } else {
            this.dataSource = {
              filter: ["!", ["statusDisplay", "=", e.value]]
            }
          }
          var toolBarItemStatus = this.toolbarItems.find(item => item.options.name == 'Status');
          toolBarItemStatus.options.value = e.value;
          this.grid.instance.repaint();
        }
      }
    });

    e.toolbarOptions.items.unshift({
      location: 'after',
      widget: 'dxSelectBox',
      options: {
        label: "Filter: ",
        name: "ApproveBy",
        placeholder: "Approved By",
        showClearButton: "true",
        items: this.managersFullNames,
        value: this.managersFullNames[4],
        onValueChanged: (e) => {
          if (e.value != null) {
            var toolBarItemStatus = this.toolbarItems.find(item => item.options.name == 'Status');
            if ((this.startDate == "" && this.endDate == "") || toolBarItemStatus.options.value != null) {
              this.getIntialDataSource();
              toolBarItemStatus.options.value = null;
            }
            this.dataSource = {
              filter: ([
                ["!", "statusDisplay", "=", e.value]
              ])
            }

            if (e.value != 'All') {
              this.dataSource ={
                filter: ([
                  ["statusDisplay", "=", "Approved"],
                  "and",
                  ["approveByFullName", "=", e.value]
                ])
              }
            }
            else {
              this.dataSource = {
                filter: ["statusDisplay", "=", "Approved"]
              }
            }
          } else {
            this.dataSource = {
              filter: ["!",["statusDisplay", "=", e.value]]
            }
          }
          var toolBarItemApproveBy = this.toolbarItems.find(item => item.options.name == 'ApproveBy');
          toolBarItemApproveBy.options.value = e.value;
          this.grid.instance.repaint();
        }
      }
    });

    e.toolbarOptions.items.unshift({
      location: 'after',
      widget: 'dxDateBox',
      options: {
        placeholder: "End Date",
        type: "date",
        max: this.maxDate,
        showClearButton: "true",
        onValueChanged: (e) => {
          this.endDate = e.value != null ? formatDate(e.value, 'yyyy/MM/dd HH:mm:ss', 'en-US') : "";
          if (this.startDate == "" && this.endDate == "") {
            this.getIntialDataSource();
            //return;
          }
          var endDateRequest = this.endDate != "" ? "endDate=" + this.endDate : "";
          var startDateRequest = this.startDate != "" ? "startDate=" + this.startDate + "&" : "";
          var toolBarItemStartDate = this.toolbarItems.find(item => item.options.placeholder == 'Start Date');
          var toolBarItemEndDate = this.toolbarItems.find(item => item.options.placeholder == 'End Date');
          var toolBarItemEndDate;
          if (e.value != null || this.startDate != "") {
            this.dataSource =  {
              store: AspNetData.createStore({
                key: 'id',
                loadUrl: this.baseUrl + 'api/InternalOrder/GetInternalOdersByDateCreated?' + startDateRequest + endDateRequest,
                updateUrl: this.baseUrl + 'api/InternalOrder',
                insertUrl: this.baseUrl + 'api/InternalOrder',
                deleteUrl: this.baseUrl + 'api/InternalOrder',
                onInserted: () => {
                  this.globalMethodsService.ToastInsertSuccessMessage(this.pageName);
                },
                onUpdated: () => {
                  this.globalMethodsService.ToastUpdateSuccessMessage(this.pageName);
                },
                onRemoved: () => {
                  this.globalMethodsService.ToastDeleteSuccessMessage(this.pageName);
                }
              }),
              paginate: true,
            }
          }
          if (this.endDate != "") {
            toolBarItemStartDate.options.max = e.value;
            toolBarItemEndDate.options.value = e.value;
          }
          else {
            toolBarItemStartDate.options.max = this.maxDate;
            toolBarItemEndDate.options.max = this.maxDate;
            toolBarItemEndDate.options.value = e.value;
          }
          var toolBarItemStatus = this.toolbarItems.find(item => item.options.name == 'Status').options.value;
          var toolBarItemApproveBy = this.toolbarItems.find(item => item.options.name == 'ApproveBy').options.value;
          if (toolBarItemStatus != null && toolBarItemStatus != 'All') {
            this.dataSource = {
              filter: ["statusDisplay", "=", toolBarItemStatus]
            }
          } else if (toolBarItemApproveBy && toolBarItemApproveBy != null) {
            this.dataSource = {
              filter: ([
                ["statusDisplay", "=", "Approved"],
                "and",
                ["approveByFullName", "=", toolBarItemApproveBy]
              ])
            }
          }
          this.grid.instance.repaint();
        }
      }
    });
    e.toolbarOptions.items.unshift({
      location: 'after',
      widget: 'dxDateBox',
      options:
      {
        placeholder: "Start Date",
        type: "date",
        max: new Date(),
        showClearButton:"true",
        onValueChanged: (e) => {
          this.startDate = e.value != null ? formatDate(e.value, 'yyyy/MM/dd HH:mm:ss', 'en-US') : "";
          var toolBarItemEndDate = this.toolbarItems.find(item => item.options.placeholder == 'End Date');
          var toolBarItemStartDate = this.toolbarItems.find(item => item.options.placeholder == 'Start Date');

          var toolBarItemStatus = this.toolbarItems.find(item => item.options.name == 'Status').options.value;
          var toolBarItemApproveBy = this.toolbarItems.find(item => item.options.name == 'ApproveBy').options.value;

          if (this.endDate == "" && this.startDate == "") {
            this.getIntialDataSource();
            //return;
          }
          var startDateRequest = this.startDate != "" ? "startDate=" + this.startDate : "";
          var endDateRequest = this.endDate != "" ? this.startDate != "" ? "&endDate=" + this.endDate : "endDate=" + this.endDate : "";
          if (e.value != null || this.endDate != "") {
            this.dataSource = {
              store: AspNetData.createStore({
                key: 'id',
                loadUrl: this.baseUrl + 'api/InternalOrder/GetInternalOdersByDateCreated?' + startDateRequest + endDateRequest,
                updateUrl: this.baseUrl + 'api/InternalOrder',
                insertUrl: this.baseUrl + 'api/InternalOrder',
                deleteUrl: this.baseUrl + 'api/InternalOrder',
                onInserted: () => {
                  this.globalMethodsService.ToastInsertSuccessMessage(this.pageName);
                },
                onUpdated: () => {
                  this.globalMethodsService.ToastUpdateSuccessMessage(this.pageName);
                },
                onRemoved: () => {
                  this.globalMethodsService.ToastDeleteSuccessMessage(this.pageName);
                }


              }),
              paginate: true,
            }

            
          }
          //Updating the tool bar items
          if (this.startDate != "") {
            toolBarItemEndDate.options.min = e.value;
            toolBarItemStartDate.options.value = e.value;
          }
          else {
            toolBarItemEndDate.options.min = e.value;
            toolBarItemStartDate.options.value = e.value;
          }

          //in case we have Approved by or different status selected
          if (toolBarItemStatus != null && toolBarItemStatus != 'All') {
            this.dataSource = {
              filter: ["statusDisplay", "=", toolBarItemStatus]
            }
          } else if (toolBarItemApproveBy && toolBarItemApproveBy != null) {
            this.dataSource = {
              filter: ([
                ["statusDisplay", "=", "Approved"],
                "and",
                ["approveByFullName", "=", toolBarItemApproveBy]
              ])
            }
          }
          this.grid.instance.repaint();
        }
      }
      
    });
    e.toolbarOptions.items.unshift({
      location: 'after',
      widget: 'dxButton',
      options: {
        icon: 'plus',
        text: 'Add Order',
        type: "default",
        useSubmitBehavior: true,
        onClick: () => {
          this.action = 1;
          this.router.navigate([`/orders-modify`], { state: { id: null, action: this.action } });
        }
      }
    });
    if (this.toolbarItems.length != 0) {
      e.toolbarOptions.items = this.toolbarItems;
    } else {
      this.toolbarItems = e.toolbarOptions.items;
    }
  }

  // toolbar item
 
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
        case "stockId": {
          e.editorOptions.valueExpr = undefined;
          e.editorOptions.onFocusIn = function (args) {
            args.component.open();
          }
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
      this.datagrid.instance.addRow();
    }

    if (e.event.key === "Enter" && this.savecolumn === true) {
      this.datagrid.instance.saveEditData();
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
    if (currentRowData.internalOrderItems) {
      for (var i = 0; i < currentRowData.internalOrderItems.length; i++) {
        total += currentRowData.internalOrderItems[i].total;
      }
    }
    newData.total = total + value;
    if (currentRowData.discount) {
      newData.total = ((100 - currentRowData.discount) / 100) * newData.total;
    } 
  }

  setCellValueItemPrice(newData, value, currentRowData) {
    newData.value = value.priceLookUpPrice;
    newData.originalValue = value.priceLookUpPrice;
    newData.stockId = value.id;
    if (currentRowData.quantity) {
      newData.total = value.priceLookUpPrice * currentRowData.quantity;
    } else {
      newData.total = 0;
    }
  }

  setCellValueDiscount(newData, value, currentRowData) {
    newData.discount = value;
    var total = 0;
    if (currentRowData.internalOrderItems) {
      for (var i = 0; i < currentRowData.internalOrderItems.length; i++) {
        total += currentRowData.internalOrderItems[i].total;
      }
    }

    newData.total = total

    if (currentRowData.deliveryCost) {
      newData.total = newData.total + currentRowData.deliveryCost;
    }   
    newData.total = ((100 - value) / 100) * newData.total;
  }

  setCellValueQuantity(newData, value, currentRowData) {
    newData.quantity = value;
    if (currentRowData.value) {      
      newData.total = currentRowData.value * value;
    } else {
      newData.total = 0;
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

  setCellValueInternalOrderItem(newData, value, currentRowData) {
    newData.internalOrderItems = value;
    var deliveryCost = 0;
    var total = 0;
    if (currentRowData.deliveryCost) {
      deliveryCost = currentRowData.deliveryCost;
    }

    for (var i = 0; i < value.length; i++) {
      total += value[i].total;
    }
    newData.total = total + deliveryCost;
    if (currentRowData.discount) {
      newData.total = ((100 - currentRowData.discount) / 100) * newData.total;
    }    
  }

  onVoidUpdateIncrease(pricelookupId) {
    this.priceIncreaseService.getItemInfo(pricelookupId).subscribe(data => {
      this.dataSource = data;
      if (data != "") {
        this.priceIncrease.increaseTypeId = data[0].increaseTypeId;
        this.priceIncrease.increase = data[0].increase;
        this.priceIncrease.date = data[0].date;
        this.priceIncrease.comment = data[0].comment;
        this.priceIncrease.priceLookUpId = pricelookupId;
        this.priceIncrease.username = this.username;
        this.pricelookupService.putPriceLookUp(this.priceIncrease).subscribe(data => {
          if (data != "") {
            notify("Price Increase added to item.", 'success', 5000);
            //this.popupVisiblePriceIncrease = false;
            this.priceIncrease = new priceIncrease();
            this.globalMethodsService.priceLookBadgeRefresh(true);
            this.orderService.getMonitoredPendingApproval(this.orderId).subscribe(data => {
              if (data != null) {
                this.priceIncreases = data;
              }
            }, subError => {
              notify(subError.error, 'error', 5000);
            });            
          } else {
            notify("Price Increase not added to item.", 'error', 5000);
          }
        }, subError => {
          notify(subError.error, 'error', 5000);
        });

      } else {
        notify("Item does not have a price increase reminder.", 'error', 5000);
      }
    });
  }

  onHidden(e) {
    //reload internal orders
    this.dataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/InternalOrder'
      })
    }
            /////////
  }
/* --------------- */
}
