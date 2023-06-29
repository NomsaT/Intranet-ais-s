import { AfterViewInit, Component, Inject, Input, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DxDataGridComponent } from 'devextreme-angular';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import DataSource from 'devextreme/data/data_source';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { StockService } from '../../core/services/stock.service';

@Component({
  selector: 'app-stores',
  templateUrl: './stores.component.html',
  styleUrls: ['./stores.component.scss']
})
export class StoresComponent implements AfterViewInit {
  dataSource: DataSource;
  StoreTypeForeignDataSource: any;
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  @Input() key: number;
  baseUrl: string;
  pageName: string = "Store";
  stockDropdown: any;
  rowIndex = 0;
  allowModify = false;

  @ViewChild('content') content;
  @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;
  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private stockService: StockService, private globalMethodsService: GlobalMethodsService, private authService: AuthenticationService) {
    this.baseUrl = baseUrl;
    this.rowIndex = 0;
    this.allowModify = this.authService.HavePermission(24);
  }

  ngAfterViewInit() {

    this.dataSource = new DataSource({
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/Stores',
        updateUrl: this.baseUrl + 'api/Stores',
        insertUrl: this.baseUrl + 'api/Stores',
        deleteUrl: this.baseUrl + 'api/Stores',
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
      filter: ["plantLocationId", "=", this.key]
    });

    this.StoreTypeForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/StoreTypes',
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
    e.data.plantLocationId = this.key;   
  }

  newRow(e) {
  }

  EditStart(e) {
    this.rowIndex = this.dataGrid.instance.getRowIndexByKey(e.data.id);
  }

  SavingStart(e) {
    this.rowIndex = 0;
  }

}
