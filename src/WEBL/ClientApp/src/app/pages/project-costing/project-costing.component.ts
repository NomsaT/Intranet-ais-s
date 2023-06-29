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
import { DxDataGridComponent } from 'devextreme-angular';

@Component({
  selector: 'app-project-costing',
  templateUrl: './project-costing.component.html',
  styleUrls: ['./project-costing.component.scss']
})
export class ProjectCostingComponent implements OnInit {
  dataSource: any;
  dataSourceClose: any;
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  pageName: string = "Project Costing";
  event: any;
  CancelDownload = true;
  popupDownloadConfirmVisible = false;
  allowModify = false;
  currencyZAR: any;
  OpenGrid = true;
  CloseGrid = false;
  Page = "Open Projects";
  layouts: string[];
  layoutValue = 0;

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
            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Project-Costing-List.xlsx');
          });
      });
    }
  };

  @ViewChild('content') content;
  @ViewChild("openProjects", { static: false }) OpendataGrid: DxDataGridComponent;
  @ViewChild("closeProjects", { static: false }) ClosedataGrid: DxDataGridComponent;
  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private globalMethodsService: GlobalMethodsService, private authService: AuthenticationService) {
    this.currencyZAR = currencyZAR;
    this.layouts = ['Open', 'Close'];

    this.dataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/ProjectCosting',
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
        loadUrl: baseUrl + 'api/ProjectCosting',
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

    this.allowModify = this.authService.HavePermission(125);
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

  onCellPrepared(e) {
    if (e.rowType === "data" && e.column.caption == "Remaining Budget" || e.column.caption == "Profit") {

      if (e.data.budget > e.data.allInternalOrderTotals) {
        e.cellElement.style.color = "#34c38f";
      }

      if (e.data.allInternalOrderTotals > e.data.budget) {
        e.cellElement.style.color = "#f46a6a";
      }
    }

    if (e.rowType === "data" && e.column.caption == "Days Left") {

      if (e.data.daysLeft > 0) {
        e.cellElement.style.color = "#34c38f";
      }

      if (e.data.daysLeft < 0) {
        e.cellElement.style.color = "#f46a6a";
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
