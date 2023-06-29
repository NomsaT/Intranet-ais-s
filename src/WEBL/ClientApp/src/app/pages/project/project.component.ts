import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import { exportDataGrid } from 'devextreme/excel_exporter';
import notify from 'devextreme/ui/notify';
import { Workbook } from 'exceljs';
import saveAs from 'file-saver';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { currencyZAR } from 'src/globalConstants';
import { OrdersService } from '../../core/services/orders.service';
import { DxDataGridComponent } from 'devextreme-angular';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.scss']
})
export class ProjectComponent implements OnInit {
  dataSource: any;
  dataSourceClose: any;
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  pageName: string = "Project";
  event: any;
  CancelDownload = true;
  popupDownloadConfirmVisible = false;
  allowModify = false;
  currencyZAR: any;
  currency = "ZAR";
  allowClose = false;
  projectId: any;
  layouts: string[];
  OpenGrid = true;
  CloseGrid = false;
  layoutValue = 0;
  Page = "Open Projects";

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
            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Project-List.xlsx');
          });
      });
    }
  };

  @ViewChild('content') content;
  @ViewChild("openProjects", { static: false }) OpendataGrid: DxDataGridComponent;
  @ViewChild("closeProjects", { static: false }) ClosedataGrid: DxDataGridComponent;
  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private orderService: OrdersService, private globalMethodsService: GlobalMethodsService, private authService: AuthenticationService) {
    this.currencyZAR = currencyZAR;
    this.layouts = ['Open', 'Close'];

    this.dataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Project',
        updateUrl: baseUrl + 'api/Project',
        insertUrl: baseUrl + 'api/Project',
        deleteUrl: baseUrl + 'api/Project',
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
      filter: ["projectStatusId", "=", 1],
      sort: ([
        { selector: "type", desc: false }
      ])
    }

    this.dataSourceClose = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Project',
        updateUrl: baseUrl + 'api/Project',
        insertUrl: baseUrl + 'api/Project',
        deleteUrl: baseUrl + 'api/Project',
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
      filter: ["projectStatusId", "=", 2],
      sort: ([
        { selector: "type", desc: false }
      ])
    }

    this.allowModify = this.authService.HavePermission(124);
    this.allowClose = this.authService.HavePermission(128);
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

  newRow(e) {
    e.data.budget = 0;
  }

  ClosePrepared(e) {
    if (e.rowType === "data" && e.column.caption == "Close Project") {
      e.cellElement.style.color = "#ffffff";
      e.cellElement.style.backgroundColor = "#f46a6a";
    }
  }

  CloseOnClick(e) {
    if (e.rowType === "data" && e.column.caption == "Close Project") {
      this.projectId = e.data.id;
      this.orderService.CloseProject(this.projectId).subscribe(data => {
        if (data != null) {
          this.OpendataGrid.instance.refresh();
          notify("Project Close.", 'success', 5000);
        } else {
          notify("Failed to Close Project", 'error', 5000);
        }
      }, subError => {
        notify(subError.error, 'error', 5000);
      });
    }
  }

  onValueChanged(e) {
    switch (e.value) {
      case "Open": {
        this.OpendataGrid.instance.refresh();
        this.OpenGrid = true;
        this.CloseGrid = false;
        this.Page = "Open Projects";
        break;
      }
      case "Close": {
        this.ClosedataGrid.instance.refresh();
        this.OpenGrid = false;
        this.CloseGrid = true;
        this.Page = "Close Projects";
        break;
      }
    }
  }
}
