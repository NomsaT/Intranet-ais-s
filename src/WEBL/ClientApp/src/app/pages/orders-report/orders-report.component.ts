import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DxDataGridComponent } from 'devextreme-angular';
import { DxoLookupComponent } from 'devextreme-angular/ui/nested';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import { currencyZAR } from 'src/globalConstants';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { StockService } from '../../core/services/stock.service';

@Component({
  selector: 'app-orders-report',
  templateUrl: './orders-report.component.html',
  styleUrls: ['./orders-report.component.scss']
})
export class OrdersReportComponent implements OnInit {
  dataSource: any;
  UsersForeignDataSource: any;
  SupplierForeignDataSource: any;
  PlantLocationForeignDataSource: any;
  DepartmentForeignDataSource: any;
  StockForeignDataSource: any;
  StorageTypeForeignDataSource: any;
  GLCodeForeignDataSource: any;
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  pageName: string = "Order";
  event: any;
  CancelDownload = true;
  popupDownloadConfirmVisible = false;
  allowModify = false;
  allowView = false;
  baseUrl: string;
  currentDeleteItem: any;
  popupDeleteItemtVisible: boolean = false;
  savecolumn = false;
  popupVisible = false;
  StorageTypeObj: any;
  currencyZAR: any;
  userId: any;
  columnIndex: any;
  selectedRowIndex = -1;
  action: any;
  heading = "Internal Order";
  statuses: string[];
  orderId: any;
  internalComment: any;
  hasComment = false;
  filterFinish = false;

  @ViewChild('content') content;
  @ViewChild("orders", { static: false }) grid: DxDataGridComponent;
  @ViewChild("OrderItem", { static: false }) datagrid: DxDataGridComponent;
  @ViewChild("stockId", { static: false }) lookup: DxoLookupComponent;
  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private globalMethodsService: GlobalMethodsService, private authService: AuthenticationService, private stockService: StockService, private router: Router, private route: ActivatedRoute) {
    this.baseUrl = baseUrl;
    this.currencyZAR = currencyZAR;
    this.hasComment = false;
    this.filterFinish = false;
    this.heading = "Internal Order";
    this.statuses = ['All', 'Approved', 'Denied'];

    this.route.queryParamMap.subscribe(queryParams => {
      this.orderId = queryParams.get('orderId');
      this.internalComment = queryParams.get('internalComment');
    });

    this.stockService.GetStorageType().subscribe(data => this.StorageTypeObj = data);

    this.authService.currentUserSubject.subscribe(user => {
      if (user) {
        this.userId = user.id
      }
    })

    this.dataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/OrdersReport',
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
      paginate: true
    }

    this.allowModify = this.authService.HavePermission(59);
    this.allowView = this.authService.HavePermission(78);
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

  editRow() {
    this.hasComment = false;
    if (this.internalComment != null) {
      this.hasComment = true;
    }
    this.grid.instance.editRow(this.selectedRowIndex);
    this.grid.instance.deselectAll();
  }

  getFullGLName(rowData) {
    return rowData.code + " - " + rowData.name;
  }

  getStock(rowData) {
    if (rowData) {
      return rowData.code + " - " + rowData.internalProductName;
    } else {
      return "";
    }
  }

  /* Internal Order */
  selectedChanged(e) {
    if (this.allowView && e.selectedRowsData[0].statusDisplay == "Approved") {
      this.selectedRowIndex = e.component.getRowIndexByKey(e.selectedRowKeys[0]);
      this.internalComment = e.selectedRowsData[0].internalComment;
      this.orderId = e.selectedRowsData[0].id;
      this.action = 2;
      this.router.navigate(['/orders-report-view'], { state: { id: this.orderId, action: this.action,canEdit:true } });
    } else {
      this.selectedRowIndex = e.component.getRowIndexByKey(e.selectedRowKeys[0]);
      this.internalComment = e.selectedRowsData[0].internalComment;
      this.orderId = e.selectedRowsData[0].id;
      this.action = 2;
      this.router.navigate(['/orders-report-view'], { state: { id: this.orderId, action: this.action, canEdit: false } });
    }
  }

  onCellPrepared(e) {
    if (e.rowType === 'data') {
      e.cellElement.style.backgroundColor = "#ffffff";
    }

    if (e.rowType === "data" && e.column.command === "edit") {

      e.cellElement.find(".dx-link:contains(Edit)").text("View");
    }  
  }

  onEditingStartParent(e) {
    this.heading = "Internal Order #" + e.data.id;
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
            this.grid.instance.filter(['statusDisplay', '=', e.value]);
          }
        }
      }
    });
  }

  onContentReady(e) {
    if (this.orderId != null && this.filterFinish == false) {
      this.filterFinish = true;
      this.grid.instance.filter(["id", "=", this.orderId]);      
    }
    if (this.orderId != null && this.filterFinish == true) {

      setTimeout(() => {
        this.grid.instance.editRow(0);
      }, 500);
          
      this.hasComment = false;
      if (this.internalComment != null) {
        this.hasComment = true;
      }
      this.filterFinish = false;
      this.orderId = null;

      setTimeout(() => {
        this.grid.instance.clearFilter();
      }, 3000);
    }
    
  }


  onEditCanceling(e) {    
  }
  /* -------------- */

  /* Internal Order Items*/
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
  /* ------------------- */
}
