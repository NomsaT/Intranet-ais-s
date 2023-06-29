import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DxDataGridComponent, DxFormComponent } from 'devextreme-angular';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import DataSource from 'devextreme/data/data_source';
import { exportDataGrid } from 'devextreme/excel_exporter';
import notify from 'devextreme/ui/notify';
import { Workbook } from 'exceljs';
import saveAs from 'file-saver';
import { postalPattern } from 'src/globalConstants';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { StockService } from '../../core/services/stock.service';
import { BarcodesService } from '../../core/services/barcodes.service';

@Component({
  selector: 'app-bins',
  templateUrl: './bins.component.html',
  styleUrls: ['./bins.component.scss']
})
export class BinsComponent implements OnInit {
  dataSource: DataSource;
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  pageName: string = "Bins";
  event: any;
  CancelDownload = true;
  popupDownloadConfirmVisible = false;
  allowModify = false;
  rowIndex = 0;
  tempAddress: any;
  postalPattern: any;
  LocationId = null;
  StoreForeignDataSource: any;
  StoreForeignDataSourceAll: any;
  LocationForeignDataSource: any;
  baseUrl: string;

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
            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Bank-Names-List.xlsx');
          });
      });
    }
  };

  @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;
  @ViewChild('content') content;
  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private stockService: StockService, private barcodesService: BarcodesService, private globalMethodsService: GlobalMethodsService, private authService: AuthenticationService) {
    this.rowIndex = 0;
    this.postalPattern = postalPattern;
    this.baseUrl = baseUrl;

    this.setStoreValue = this.setStoreValue.bind(this);

    this.dataSource = new DataSource({
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Bins',
        updateUrl: baseUrl + 'api/Bins',
        insertUrl: baseUrl + 'api/Bins',
        deleteUrl: baseUrl + 'api/Bins',
        onLoaded: function (result) {
        }.bind(this),
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
      paginate: true
    });

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

    this.StoreForeignDataSourceAll = {
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

    this.allowModify = this.authService.HavePermission(130);
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

  barcodeOnClick(e) {
    if (e.rowType === "data" && e.column.caption == "Print Barcode") {
      this.barcodesService.PrintingBinBarcode([{ Id: e.data.id, Name: e.data.name, Barcode: e.data.barcode }]).subscribe(data => {
        if (data != null && data != undefined) {
          notify("Barcode Printed", 'success', 5000);
        }
      }, subError => {
        notify(subError, 'error', 5000);
      });
    }
  }

  setStoreValue(e) {
    this.LocationId = e.value;
   

    this.StoreForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/Stores/BinStore',
      }),
      filter: ["plantLocationId", "=", this.LocationId],
      paginate: true
    }
    this.dataGrid.instance.cellValue(this.rowIndex, "plantLocationId", this.LocationId);
    this.dataGrid.instance.cellValue(this.rowIndex, "storeId", 0);
  }

  onToolbarPreparing(e): void {
    if (this.allowModify == true) {
      e.toolbarOptions.items.unshift({
        location: 'after',
        widget: 'dxButton',
        options: {
          icon: 'print',
          text: 'Print Selection',
          type: "normal",
          useSubmitBehavior: false,
          onClick: () => {
            this.barcodesService.PrintingBinBarcode(this.dataGrid.instance.getSelectedRowsData()).subscribe(data => {
              if (data != null && data != undefined) {
                this.dataGrid.instance.clearSelection();
                this.dataGrid.instance.getDataSource().reload();
              }
            }, subError => {
              notify(subError.error, 'error', 5000);
            });
          }
        }
      });
      e.toolbarOptions.items.unshift({
        location: 'after',
        widget: 'dxButton',
        options: {
          icon: 'clear',
          text: 'Clear Selection',
          type: "normal",
          useSubmitBehavior: false,
          onClick: () => {
            this.dataGrid.instance.clearSelection();
          }
        }
      });
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

  EditStart(e) {
    this.rowIndex = this.dataGrid.instance.getRowIndexByKey(e.data.id);
  }

  newRow(e) {
    this.rowIndex = 0;
  }

  onSaving(e) {
   
    this.rowIndex = 0;
  }

  fieldDataChanged(cellInfo, temp) {
    cellInfo.setValue(temp);
  }
}
