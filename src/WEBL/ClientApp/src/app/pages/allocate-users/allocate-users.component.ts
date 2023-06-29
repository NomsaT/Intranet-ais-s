import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DxDataGridComponent } from 'devextreme-angular';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import { exportDataGrid } from 'devextreme/excel_exporter';
import notify from 'devextreme/ui/notify';
import { Workbook } from 'exceljs';
import saveAs from 'file-saver';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { DepartmentUserService } from '../../core/services/department-users.service';

@Component({
  selector: 'app-allocate-users',
  templateUrl: './allocate-users.component.html',
  styleUrls: ['./allocate-users.component.scss']
})
export class AllocateUsersComponent implements OnInit {
  dataSource: any
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  defaultVisible = false;
  baseUrl: any;
  pageName: string = "Allocate User";
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
            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Allocation-to-Department-List.xlsx');
          });
      });
    }
  };

  @ViewChild('content') content;
  @ViewChild("allocateUsers", { static: false }) dataGrid: DxDataGridComponent;
  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private departmentUserService: DepartmentUserService,private globalMethodsService: GlobalMethodsService) {
    this.baseUrl = baseUrl;
    this.departmentUserService.departmentAllocation.subscribe(r => {
      if (r > 0) {
        this.dataGrid.instance.refresh();
      }
      
    });
    this.dataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/AllocateUsers',
        updateUrl: baseUrl + 'api/AllocateUsers',
        insertUrl: baseUrl + 'api/AllocateUsers',
        deleteUrl: baseUrl + 'api/AllocateUsers',
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
        { selector: "name", desc: false }
      ]),
      filter: ["isDisabled", "=", 0]
    }
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

  toggleDefault() {
    this.defaultVisible = !this.defaultVisible;
  }

  selectionChanged(e) {
    e.component.collapseAll(-1);
    e.component.expandRow(e.currentSelectedRowKeys[0]);
  }

  rowChanged(e) {
    e.component.collapseAll(-1);
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

  onCellPrepared(e) {
    if (e.rowType === "data") {
      if (e.data.percentage < 100) {
        e.cellElement.style.color = "#fff";
        e.cellElement.style.backgroundColor = "#3379b7";
      }
    }
  }
}
