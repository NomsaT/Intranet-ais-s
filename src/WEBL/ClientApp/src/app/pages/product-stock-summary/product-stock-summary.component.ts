import { AfterViewInit, Component, Inject, Input, Output, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DxDataGridComponent } from 'devextreme-angular';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import DataSource from 'devextreme/data/data_source';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { StockService } from '../../core/services/stock.service';

@Component({
  selector: 'app-product-stock-summary',
  templateUrl: './product-stock-summary.component.html',
  styleUrls: ['./product-stock-summary.component.scss']
})
export class ProductStockSummaryComponent implements AfterViewInit {
  dataSource: DataSource;
  StockForeignDataSource: any;
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  @Input() key: number;
  baseUrl: string;
  pageName: string = "Product Stock";
  stockDropdown: any;
  rowIndex = 0;
  allowModify = false;
  setQtyPer: any;
  setQtyUsed: any;

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
        loadUrl: this.baseUrl + 'api/ProductStock',
        updateUrl: this.baseUrl + 'api/ProductStock',
        insertUrl: this.baseUrl + 'api/ProductStock',
        deleteUrl: this.baseUrl + 'api/ProductStock',
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
      filter: ["productId", "=", this.key]
    });

    this.StockForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/Stock',
        /* onBeforeSend: (method, param) => {
           param.headers = {
             'Authorization': 'Bearer ' + this.token
           };
         }*/
      }),
      paginate: true
    }

    this.stockDropdown = {
      onValueChanged: (e: { value: number; }) => {      
        this.dataGrid.instance.cellValue(this.rowIndex, "stockId", e.value);
      }
    }

    this.setQtyPer = {
      onValueChanged: (e: { value: number; }) => {
        var used = 0;
        var qty = 0;
        this.dataGrid.instance.cellValue(this.rowIndex, "qtyPerSquareMeter", e.value);
        used = this.dataGrid.instance.cellValue(this.rowIndex, "squaresUsed");
        qty = used * e.value;
        this.dataGrid.instance.cellValue(this.rowIndex, "quantity", qty);
      }
    }

    this.setQtyUsed = {
      onValueChanged: (e: { value: number; }) => {
        var per = 0;
        var qty = 0;
        this.dataGrid.instance.cellValue(this.rowIndex, "squaresUsed", e.value);
        per = this.dataGrid.instance.cellValue(this.rowIndex, "qtyPerSquareMeter");
        qty = per * e.value;
        this.dataGrid.instance.cellValue(this.rowIndex, "quantity", qty);
      }
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

  getStock(rowData) {
    return rowData.code + " - " + rowData.internalProductName;
  }

  getStockUom(rowData) {
    return rowData.uomName;
  }

  getStockCategory(rowData) {
    return rowData.categoryName;
  }

  insertRow(e) {
    e.data.productId = this.key;
  }

  EditStart(e) {
    this.rowIndex = this.dataGrid.instance.getRowIndexByKey(e.data.id);
  }

  SavingStart(e) {
    this.rowIndex = 0;
  }
}
