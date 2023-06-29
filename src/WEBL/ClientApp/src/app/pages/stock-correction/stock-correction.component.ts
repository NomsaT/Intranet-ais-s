import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DxDataGridComponent } from 'devextreme-angular';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import { exportDataGrid } from 'devextreme/excel_exporter';
import notify from 'devextreme/ui/notify';
import { Workbook } from 'exceljs';
import saveAs from 'file-saver';
import { currencyZAR } from 'src/globalConstants';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { StockService } from '../../core/services/stock.service';

@Component({
  selector: 'app-stock-correction',
  templateUrl: './stock-correction.component.html',
  styleUrls: ['./stock-correction.component.scss']
})
export class StockCorrectionComponent implements OnInit {
  dataSource: any;
  StockForeignDataSource: any;
  DepartmentForeignDataSource: any;
  LocationForeignDataSource: any;
  StoreForeignDataSource: any;
  CurrencyForeignDataSource: any;
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  currentColor: any;
  isActive: string;
  baseUrl: any;
  pageName: string = "Stock Item";
  currencyZAR: any;
  event: any;
  CancelDownload = true;
  popupDownloadConfirmVisible = false;
  allowModify = false;
  LocationId = null;
  rowIndex = 0;

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
            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Stock-Correction-List.xlsx');
          });
      });
    }
  };

  @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;
  @ViewChild('content') content;
  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private stockService: StockService, private globalMethodsService: GlobalMethodsService, private authService: AuthenticationService) {
    this.currencyZAR = currencyZAR;
    this.baseUrl = baseUrl;
    this.rowIndex = 0;
    this.setStoreValue = this.setStoreValue.bind(this);

    this.dataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/StockCorrection',
        updateUrl: baseUrl + 'api/StockCorrection',
        onUpdated: () => {
          this.globalMethodsService.ToastUpdateSuccessMessage(this.pageName);
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

    this.StockForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Stock',
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
      sort: [
        { selector: "Name", desc: false }
      ],
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
      sort: [
        { selector: "Name", desc: false }
      ],
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
      sort: [
        { selector: "Name", desc: false }
      ],
      paginate: true
    }

    this.CurrencyForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Currency',
      }),
      paginate: true
    }

    this.allowModify = this.authService.HavePermission(17);
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

  getStock(rowData) {
    return rowData.code + " - " + rowData.internalProductName;
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

  EditStart(e) {
    this.rowIndex = this.dataGrid.instance.getRowIndexByKey(e.data.id);
    this.LocationId = this.dataGrid.instance.cellValue(this.rowIndex, "locationId");
    this.StoreForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/Stores',
      }),
      filter: ["plantLocationId", "=", this.LocationId],
      paginate: true
    }
  }

  setStoreValue(e) {
    this.LocationId = e.value;
    this.dataGrid.instance.cellValue(this.rowIndex, "locationId", e.value);

    this.StoreForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/Stores',
      }),
      filter: ["plantLocationId", "=", this.LocationId],
      paginate: true
    }

    //set the default store after the location is selected
    this.stockService.GetDefaultStore(this.LocationId).subscribe(data => {
      if (data != null) {
        this.dataGrid.instance.cellValue(this.rowIndex, "storeId", data[0].defaultStoreId);
      } else {
        this.dataGrid.instance.cellValue(this.rowIndex, "storeId", null);
      }
    }, subError => {
      notify(subError.error, 'error', 5000);
    });
  }

  initialUomQuantity(rowData) {
    return rowData.itemQuantity + " (" + rowData.uomName + ")";
  }

  priceISO(rowData) {
    return rowData.supplierCurrencyIso + " "+ GlobalMethodsService.formatCurrency(rowData.price, "") ;
  }
}
