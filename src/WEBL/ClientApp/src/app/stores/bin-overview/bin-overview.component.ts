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
  selector: 'app-bin-overview',
  templateUrl: './bin-overview.component.html',
  styleUrls: ['./bin-overview.component.scss']
})
export class BinOverviewComponent implements OnInit {
  dataSource: any;
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  pageName: string = "Barcode List";
  event: any;
  CancelDownload = true;
  popupDownloadConfirmVisible = false;
  allowViewing= false;
  selectedRows: number[] = [];

  @ViewChild('content') content;
  @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;
  constructor(@Inject('BASE_URL') baseUrl: string, private authService: AuthenticationService) {
    this.dataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/BinOverview'
      }),
      paginate: true
    }

    this.allowViewing = this.authService.HavePermission(131);
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

  onToolbarPreparing(e): void {
  }

}
