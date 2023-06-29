import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import { exportDataGrid } from 'devextreme/excel_exporter';
import notify from 'devextreme/ui/notify';
import { Workbook } from 'exceljs';
import saveAs from 'file-saver';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';

@Component({
  selector: 'app-pstore-type',
  templateUrl: './store-type.component.html',
  styleUrls: ['./store-type.component.scss']
})
export class StoreTypeComponent implements OnInit {
  dataSource: any;
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  pageName: string = "Store Type";
  event: any;
  CancelDownload = true;
  popupDownloadConfirmVisible = false;
  allowModify = false;

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
            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Store-Types-List.xlsx');
          });
      });
    }
  };

  @ViewChild('content') content;
  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private globalMethodsService: GlobalMethodsService, private authService: AuthenticationService) {
    this.dataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/StoreTypes',
        updateUrl: baseUrl + 'api/StoreTypes',
        insertUrl: baseUrl + 'api/StoreTypes',
        deleteUrl: baseUrl + 'api/StoreTypes',
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
      paginate: true,
      sort: ([
        { selector: "type", desc: false }
      ])
    }

    this.allowModify = this.authService.HavePermission(49);
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

}
