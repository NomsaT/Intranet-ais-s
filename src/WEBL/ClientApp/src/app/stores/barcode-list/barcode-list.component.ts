import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import { exportDataGrid } from 'devextreme/excel_exporter';
import notify from 'devextreme/ui/notify';
import { Workbook } from 'exceljs';
import saveAs from 'file-saver';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { BarcodesService } from '../../core/services/barcodes.service';
import { DxDataGridComponent } from 'devextreme-angular';

@Component({
  selector: 'app-barcode-list',
  templateUrl: './barcode-list.component.html',
  styleUrls: ['./barcode-list.component.scss']
})
export class BarcodeListComponent implements OnInit {
  dataSource: any;
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  pageName: string = "Barcode List";
  event: any;
  CancelDownload = true;
  popupDownloadConfirmVisible = false;
  allowModify = false;
  selectedRows: number[] = [];

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
            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Barcode-List.xlsx');
          });
      });
    }
  };

  @ViewChild('content') content;
  @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;
  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private globalMethodsService: GlobalMethodsService, private authService: AuthenticationService, private barcodesService: BarcodesService) {
    this.dataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/PrintBarcode/getPrintBarcode'
        /*onBeforeSend: (method, param) => {
          param.headers = {
            'Authorization': 'Bearer ' + this.authService.getToken()
          };
        }*/
      }),
      paginate: true
    }

    this.allowModify = this.authService.HavePermission(96);
  }

  
  calculateCellValue(rowData) {
    return 'STQ' + rowData.barcode.slice(3).padStart(9, '0');
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

  barcodeOnClick(e) {

    let stockId: string = e.data.stockId;
    // stockBarcode = "STK" + stockBarcode.padStart(9, '0');

    if (e.rowType === "data" && e.column.caption == "Print Barcode") {
      this.barcodesService.Printing([{ Id: e.data.id, Code: e.data.code, Barcode: e.data.barcode, stockId: stockId }]).subscribe(data => {
        if (data != null && data != undefined) {
          this.barcodesService.PrintingBarcode(e.data.id).subscribe(data => {
            if (data != null && data != undefined) {
              this.dataGrid.instance.getDataSource().reload();
              this.globalMethodsService.printingBadgeRefresh(true);
            }
          }, subError => {
            notify(subError.error, 'error', 5000);
          });
        }
      }, subError => {
        notify(subError, 'error', 5000);
      });
    }
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
            this.barcodesService.Printing(this.dataGrid.instance.getSelectedRowsData()).subscribe(data => {
              if (data != null && data != undefined) {
                this.dataGrid.instance.clearSelection();
                this.dataGrid.instance.getDataSource().reload();
                this.globalMethodsService.printingBadgeRefresh(true);
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
}
