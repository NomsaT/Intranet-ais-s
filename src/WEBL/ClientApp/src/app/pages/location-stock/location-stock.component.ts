import { AfterViewInit, Component, Inject, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import { exportDataGrid } from 'devextreme/excel_exporter';
import notify from 'devextreme/ui/notify';
import { Workbook } from 'exceljs';
import saveAs from 'file-saver';
import { currencyZAR } from 'src/globalConstants';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';

@Component({
  selector: 'app-location-stock',
  templateUrl: './location-stock.component.html',
  styleUrls: ['./location-stock.component.scss']
})
export class LocationStockComponent implements AfterViewInit {
  dataSource: any;
  UnitofMeasureForeignDataSource: any;
  StockCategoryForeignDataSource: any;
  DepartmentForeignDataSource: any;
  LocationForeignDataSource: any;
  StoreForeignDataSource: any;
  StoreTypeForeignDataSource: any;
  StockGroupForeignDataSource: any;
  CurrencyForeignDataSource: any;
  StorageTypeForeignDataSource: any;
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  currencyZAR: any;
  event: any;
  CancelDownload = true;
  popupDownloadConfirmVisible = false;

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
            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'location-stock-List.xlsx');
          });
      });

    }
  };

  @ViewChild('content') content;
  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private globalMethodsService: GlobalMethodsService, private authService: AuthenticationService) {
    this.currencyZAR = currencyZAR;
    this.dataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/DepartmentStock',
        updateUrl: baseUrl + 'api/DepartmentStock',
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

    this.UnitofMeasureForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/UnitOfMeasurement',
      }),
      paginate: true
    }

    this.StockCategoryForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/StockCategory',
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

    this.LocationForeignDataSource = {
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

    this.StoreForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Stores',
        /* onBeforeSend: (method, param) => {
           param.headers = {
             'Authorization': 'Bearer ' + this.token
           };
         }*/
      }),
      paginate: true
    }

    this.StoreTypeForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/StoreTypes',
        /* onBeforeSend: (method, param) => {
           param.headers = {
             'Authorization': 'Bearer ' + this.token
           };
         }*/
      }),
      paginate: true
    }

    this.StockGroupForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/StockGroup',
        /* onBeforeSend: (method, param) => {
           param.headers = {
             'Authorization': 'Bearer ' + this.token
           };
         }*/
      }),
      paginate: true
    }

    this.CurrencyForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Currency',
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
  }

  randFormatConversion(cellInfo) {
    if (cellInfo.value)
      return GlobalMethodsService.formatCurrency(cellInfo.value, "R");
    else
      return cellInfo.value;
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

  ngAfterViewInit() {
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

  initialUomQuantity(rowData) {
    return rowData.quantity + " (" + rowData.uomName + ")";
  }

  priceISO(rowData) {
    return rowData.supplierCurrencyIso + " " + GlobalMethodsService.formatCurrency(rowData.price, "") ;
  }
}
