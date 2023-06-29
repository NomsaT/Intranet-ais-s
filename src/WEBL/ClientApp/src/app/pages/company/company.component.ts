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

@Component({
  selector: 'app-company',
  templateUrl: './company.component.html',
  styleUrls: ['./company.component.scss']
})
export class CompanyComponent implements OnInit {
  /*  dataSource: any;*/
  dataSource: DataSource;
  CountryForeignDataSource: any;
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  pageName: string = "Company";
  event: any;
  CancelDownload = true;
  popupDownloadConfirmVisible = false;
  allowModify = false;
  rowIndex = 0;
  tempAddress: any;
  postalPattern: any;

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
  @ViewChild("formAddress") formAddress: DxFormComponent;
  @ViewChild('content') content;
  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private globalMethodsService: GlobalMethodsService, private authService: AuthenticationService) {
    this.rowIndex = 0;
    this.postalPattern = postalPattern;
    this.dataSource = new DataSource({
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Company',
        updateUrl: baseUrl + 'api/Company',
        insertUrl: baseUrl + 'api/Company',
        deleteUrl: baseUrl + 'api/Company',
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

    this.CountryForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Country',
      }),
      paginate: true
    }

    this.allowModify = this.authService.HavePermission(127);
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

  EditStart(e) {
    this.rowIndex = this.dataGrid.instance.getRowIndexByKey(e.data.id);
    this.tempAddress = JSON.parse(JSON.stringify(e.data.address));  
  }

  newRow(e) {
    this.tempAddress = { countryId: 190 };
    e.data.motherCompany = false;
  }

  onSaving(e) {
    if (!this.formAddress.instance.validate().isValid) {
      e.cancel = true;
    }    
    this.rowIndex = 0;
  }

  fieldDataChanged(cellInfo, temp) {
    cellInfo.setValue(temp);
  }
}
