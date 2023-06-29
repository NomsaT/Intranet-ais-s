import { AfterViewInit, Component, Inject, Input, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DxDataGridComponent } from 'devextreme-angular';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import DataSource from 'devextreme/data/data_source';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { DepartmentUserService } from '../../core/services/department-users.service';
import { popupAutoSelectService } from '../../core/services/popupAutoSelect.service';
import notify from 'devextreme/ui/notify';

@Component({
  selector: 'app-department-users',
  templateUrl: './department-users.component.html',
  styleUrls: ['./department-users.component.scss']
})
export class DepartmentUsersComponent implements AfterViewInit {
  dataSource: DataSource;
  DepartmentForeignDataSource: any;
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  @Input() key: number;
  baseUrl: string;
  pageName: string = "User Allocated to a Department";
  userId: any;
  rowIndex = 0;
  allowModify = false;
  popupVisible = false;

  buttonOptions: any = {
    icon: "plus",
    type: "default",
    useSubmitBehavior: false,
    onClick: () => {
      this.popupVisible = true;
    }
  }

  @ViewChild('content') content;
  @ViewChild("profitcenterUsers", { static: false }) dataGrid: DxDataGridComponent;
  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private departmentUserService: DepartmentUserService, private globalMethodsService: GlobalMethodsService, private authService: AuthenticationService,private autoSelect: popupAutoSelectService,) {
    this.baseUrl = baseUrl;
    this.rowIndex = 0;
    this.allowModify = this.authService.HavePermission(31);
  }

  ngAfterViewInit() {

    this.DepartmentForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/Department',
        /* onBeforeSend: (method, param) => {
           param.headers = {
             'Authorization': 'Bearer ' + this.token
           };
         }*/
      }),
      sort: [
        { selector: "AbbAndName", desc: false }
      ],
      paginate: true
    }

    this.dataSource = new DataSource({
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/DepartmentUsers',
        updateUrl: this.baseUrl + 'api/DepartmentUsers',
        insertUrl: this.baseUrl + 'api/DepartmentUsers',
        deleteUrl: this.baseUrl + 'api/DepartmentUsers',
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
      filter: ["userId", "=", this.key]
    });

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
    e.data.userId = this.key;
  }

  EditStart(e) {
    this.rowIndex = this.dataGrid.instance.getRowIndexByKey(e.data.id);
  }

  SavingStart(e) {
    this.rowIndex = 0;    
  }

  onSaved(e) {
    this.departmentUserService.departmentAllocation.next(1);
  }

  onHidden(e) {
    this.dataGrid.instance.getDataSource().reload();
    this.autoSelect.GetNewestDepartment().subscribe(data => {
      this.dataGrid.instance.cellValue(this.rowIndex, "departmentId", data);
    }, subError => {
      notify(subError.error, 'error', 5000);
    }); 
  }
}
