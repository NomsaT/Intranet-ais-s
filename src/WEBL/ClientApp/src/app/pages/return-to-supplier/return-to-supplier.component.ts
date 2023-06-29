import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import { exportDataGrid } from 'devextreme/excel_exporter';
import notify from 'devextreme/ui/notify';
import { Workbook } from 'exceljs';
import saveAs from 'file-saver';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { currencyZAR, EnumRecipe } from 'src/globalConstants';
import { QuarantineService } from '../../core/services/quarantine.service';
import { DxDataGridComponent, DxFormComponent } from 'devextreme-angular';

@Component({
  selector: 'app-return-to-supplier',
  templateUrl: './return-to-supplier.component.html',
  styleUrls: ['./return-to-supplier.component.scss']
})
export class ReturnToSupplierComponent implements OnInit {
  dataSource: any;
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  pageName: string = "Bank Name";
  event: any;
  CancelDownload = true;
  popupDownloadConfirmVisible = false;
  allowLog = false;
  currencyZAR: any;

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
  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private globalMethodsService: GlobalMethodsService, private authService: AuthenticationService, private quarantineService: QuarantineService) {
    this.currencyZAR = currencyZAR;
    this.dataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/ReturnToSupplier',
        /*onBeforeSend: (method, param) => {
          param.headers = {
            'Authorization': 'Bearer ' + this.authService.getToken()
          };
        }*/
      }),
      paginate: true,
      sort: ([
        { selector: "type", desc: false }
      ])
    }
    this.allowLog = this.authService.HavePermission(121);
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

    if (this.allowLog == true) {
      e.toolbarOptions.items.unshift({
        location: 'after',
        widget: 'dxButton',
        options: {
          icon: 'movetofolder',
          text: 'Log Selection',
          type: "normal",
          useSubmitBehavior: false,
          onClick: () => {
            this.quarantineService.LogReturnToSupplier(this.dataGrid.instance.getSelectedRowsData()).subscribe(data => {
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
}
