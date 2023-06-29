import { AfterViewInit, Component, Inject, Input, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DxDataGridComponent } from 'devextreme-angular';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import DataSource from 'devextreme/data/data_source';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { StockService } from '../../core/services/stock.service';

@Component({
  selector: 'app-department-managers-users',
  templateUrl: './department-managers-users.component.html',
  styleUrls: ['./department-managers-users.component.scss']
})
export class DepartmentManagerUsersComponent implements AfterViewInit {
  dataSource: DataSource;
  CostManagersForeignDataSource: any;
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  @Input() key: number;
  baseUrl: string;
  pageName: string = "Department Manager";
  rowIndex = 0;
  allowModify = false;

  @ViewChild('content') content;
  @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;
  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private stockService: StockService, private globalMethodsService: GlobalMethodsService, private authService: AuthenticationService) {
    this.baseUrl = baseUrl;
    this.rowIndex = 0;
    this.allowModify = this.authService.HavePermission(7);
  }

  ngAfterViewInit() {

    this.dataSource = new DataSource({
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/DepartmentManager',
        updateUrl: this.baseUrl + 'api/DepartmentManager',
        insertUrl: this.baseUrl + 'api/DepartmentManager',
        deleteUrl: this.baseUrl + 'api/DepartmentManager',
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
         *//* param.headers = {
            'Authorization': 'Bearer ' + this.authService.getToken()
          };*//*
        }*/
      }),
      paginate: true,
      filter: ["departmentId", "=", this.key]
    });

    this.CostManagersForeignDataSource = {
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

 insertRow(e) {
    e.data.departmentId = this.key;
  }

  EditStart(e) {
    this.rowIndex = this.dataGrid.instance.getRowIndexByKey(e.data.id);
  }

  SavingStart(e) {
    this.rowIndex = 0;
  }

}
