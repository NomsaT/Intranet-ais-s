import { AfterViewInit, Component, Inject, Input, Output, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DxDataGridComponent } from 'devextreme-angular';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import DataSource from 'devextreme/data/data_source';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { StockService } from '../../core/services/stock.service';

@Component({
  selector: 'app-product-item-summary',
  templateUrl: './product-item-summary.component.html',
  styleUrls: ['./product-item-summary.component.scss']
})
export class ProductItemSummaryComponent implements AfterViewInit {
  dataSource: DataSource;
  ProductForeignDataSource: any;
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  @Input() key: number;
  baseUrl: string;
  pageName: string = "Product Item";
  DepartmentDropdown: any;
  rowIndex = 0;
  allowModify = false;
/*  setQtyPer: any;
  setQtyUsed: any;*/

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
        loadUrl: this.baseUrl + 'api/ProductItem',
        updateUrl: this.baseUrl + 'api/ProductItem',
        insertUrl: this.baseUrl + 'api/ProductItem',
        deleteUrl: this.baseUrl + 'api/ProductItem',
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
      filter: ["productId", "=", this.key]
    });

    this.ProductForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/Products',
      }),
      paginate: true
    }  

    this.DepartmentDropdown = {
      onValueChanged: (e: { value: number; }) => {
        this.dataGrid.instance.cellValue(this.rowIndex, "itemId", e.value);
      }
    }

/*    this.setQtyPer = {
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
    }*/

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

  getProduct(rowData) {
    return rowData.productCode + " - " + rowData.productName;
  }

  getDepartment(rowData) {
    return rowData.departmentFullName
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
