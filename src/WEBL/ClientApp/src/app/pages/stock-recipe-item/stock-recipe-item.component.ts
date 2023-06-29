import { AfterViewInit, Component, Inject, Input, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DxDataGridComponent } from 'devextreme-angular';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import DataSource from 'devextreme/data/data_source';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { StockService } from '../../core/services/stock.service';

@Component({
  selector: 'app-stock-recipe-item',
  templateUrl: './stock-recipe-item.component.html',
  styleUrls: ['./stock-recipe-item.component.scss']
})
export class StockRecipeItemComponent implements AfterViewInit {
  dataSource: DataSource;
  StockForeignDataSource: any;
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  @Input() key: number;
  baseUrl: string;
  pageName: string = "Stock Recipe Item";
  stockDropdown: any;
  rowIndex = 0;
  allowModify = false;

  @ViewChild('content') content;
  @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;
  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private stockService: StockService, private globalMethodsService: GlobalMethodsService, private authService: AuthenticationService) {
    this.baseUrl = baseUrl;
    this.rowIndex = 0;
  }

  ngAfterViewInit() {

    this.dataSource = new DataSource({
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/StockRecipeItem',
        /*onBeforeSend: (method, param) => {
         *//* param.headers = {
            'Authorization': 'Bearer ' + this.authService.getToken()
          };*//*
        }*/
      }),
      paginate: true,
      filter: ["stockId", "=", this.key]
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

}
