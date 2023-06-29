import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DxDataGridComponent } from 'devextreme-angular';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import { exportDataGrid } from 'devextreme/excel_exporter';
import notify from 'devextreme/ui/notify';
import { Workbook } from 'exceljs';
import saveAs from 'file-saver';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { popupAutoSelectService } from '../../core/services/popupAutoSelect.service';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.scss']
})
export class DepartmentComponent implements OnInit {
  dataSource: any;
  baseUrl: any;
  DepartmentManagerForeignDataSource: any;
  CostTypeForeignDataSource: any;
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  popupVisibleCM = false;
  popupVisiblePC = false;
  popupVisible = false;
  isActive: string;
  pageName: string = "Department";
  event: any;
  CancelDownload = true;
  popupDownloadConfirmVisible = false;
  allowModify = false;
  currentColor: any;
  rowIndex = 0;

  buttonDep: any = {
    icon: "plus",
    text: "Department Manager",
    type: "default",
    useSubmitBehavior: false,
    onClick: () => {
      this.popupVisibleCM = true;
    }
  }

  buttonOptions: any = {
    icon: "plus",
    type: "default",
    hint: "Add a new cost type",
    useSubmitBehavior: false,
    onClick: () => {
      this.popupVisible = true;
    }
  }

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
            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Cost-Centers-List.xlsx');
          });
      });
    }
  };

  @ViewChild('content') content;
  @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;
  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private globalMethodsService: GlobalMethodsService, private autoSelect: popupAutoSelectService, private authService: AuthenticationService) {
    this.baseUrl = baseUrl;
    this.rowIndex = 0;

    this.dataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Department/getDepartmentMain',
        updateUrl: baseUrl + 'api/Department',
        insertUrl: baseUrl + 'api/Department',
        deleteUrl: baseUrl + 'api/Department',
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
      ])
    }

    this.DepartmentManagerForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/FullEmployeeName/DepartmentManagers',
        /* onBeforeSend: (method, param) => {
           param.headers = {
             'Authorization': 'Bearer ' + this.token
           };
         }*/
      }),
      paginate: true
    }

    this.CostTypeForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/CostTypes',
      }),
      sort: [
        { selector: "AbbName", desc: false }
      ],
      paginate: true
    }

    this.allowModify = this.authService.HavePermission(9);
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

  getFullCostTypeName(rowData) {
    return rowData.abbreviation + " - " + rowData.name;
  }


  onHidden(e) {
    this.dataGrid.instance.getDataSource().reload();
    this.autoSelect.GetNewestCostType().subscribe(data => {
      this.dataGrid.instance.cellValue(this.rowIndex, "costTypeId", data);
    }, subError => {
      notify(subError.error, 'error', 5000);
    }); 
  }

  handleValueChange(e, cell) {
    this.currentColor = e.value;
    var color = e.value;
    this.dataGrid.instance.cellValue(cell.row.rowIndex, "color", color);
  }

  EditStart(e) {
    this.currentColor = e.data.color;
    this.rowIndex = this.dataGrid.instance.getRowIndexByKey(e.data.id);
  }

  newRow(e) {
    this.currentColor = "#000000";
    e.data.generalPurchasing = false;
  }

  onCellPrepared(e) {
    if (e.rowType === "data" && e.column.caption == "Department Color") {
      if (e.data.color != null) {
        e.cellElement.innerHTML = '<div style="height: 100%; width: 100%; margin-left: 50%; margin-right:50%;"><div style="height: 15px; width:15px; border-radius: 50%; border:1px solid ' + e.data.color + '; background-color: ' + e.data.color + '; display: "inline";></div></div>';
      } else {
        e.cellElement.style.backgroundColor = "#ffffff";
      }
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

  onSaving(e) {
    this.rowIndex = 0;
  }
}
