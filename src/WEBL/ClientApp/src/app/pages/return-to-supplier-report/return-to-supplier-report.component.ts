import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import { exportDataGrid } from 'devextreme/excel_exporter';
import notify from 'devextreme/ui/notify';
import { Workbook } from 'exceljs';
import saveAs from 'file-saver';
import { GlobalMethodsService } from 'src/app/core/services/global-methods.service';
import { currencyZAR } from 'src/globalConstants';

@Component({
  selector: 'app-return-to-supplier-report',
  templateUrl: './return-to-supplier-report.component.html',
  styleUrls: ['./return-to-supplier-report.component.scss']
})
export class ReturnToSupplierReportComponent implements OnInit {
  dataSource: any;
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
            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Service-Items-Report-List.xlsx');
          });
      });
    }
  };

  @ViewChild('content') content;
  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal) {
    this.currencyZAR = currencyZAR;
    this.dataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/ReturnToSupplierLogReport',
      }),
      paginate: true,
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

  priceISO(rowData) {
    return rowData.supplierCurrency + " " + GlobalMethodsService.formatCurrency(rowData.value,"") ;
  }
}
