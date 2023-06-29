import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DxDataGridComponent, DxFormComponent } from 'devextreme-angular';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import { exportDataGrid } from 'devextreme/excel_exporter';
import notify from 'devextreme/ui/notify';
import { Workbook } from 'exceljs';
import saveAs from 'file-saver';
import { postalPattern } from 'src/globalConstants';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';

@Component({
  selector: 'app-plant-location',
  templateUrl: './plant-location.component.html',
  styleUrls: ['./plant-location.component.scss']
})
export class PlantLocationComponent implements OnInit {
  dataSource: any;
  defaultStoreForeignDataSource: any;
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  pageName: string = "Plant Location";
  event: any;
  CancelDownload = true;
  popupDownloadConfirmVisible = false;
  allowModify = false;
  postalPattern: any;
  CountryForeignDataSource: any;
  tempAddress: any;
  defaultVisible = false;
  notEditMode = false;
  rowIndex = 0;
  baseUrl: string;
  locationId = null;
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
            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Plant-Locations-List.xlsx');
          });
      });
    }
  };

  @ViewChild('content') content;
  @ViewChild("formAddress") formAddress: DxFormComponent;
  @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;
  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private globalMethodsService: GlobalMethodsService, private authService: AuthenticationService) {
    this.baseUrl = baseUrl;
    this.postalPattern = postalPattern;
    this.rowIndex = 0;

    this.setStoreValue = this.setStoreValue.bind(this);

    this.dataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/PlantLocation',
        updateUrl: baseUrl + 'api/PlantLocation',
        insertUrl: baseUrl + 'api/PlantLocation',
        deleteUrl: baseUrl + 'api/PlantLocation',
        onInserted: () => {
          this.globalMethodsService.ToastInsertSuccessMessage(this.pageName);
        },
        onUpdated: () => {
          this.globalMethodsService.ToastUpdateSuccessMessage(this.pageName);
        },
        onRemoved: () => {
          this.globalMethodsService.ToastDeleteSuccessMessage(this.pageName);
        }
      }),
      paginate: true,
      sort: ([
        { selector: "name", desc: false }
      ])
    }

    this.CountryForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Country',
      }),
      paginate: true
    }

    this.defaultStoreForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/Stores',
        /*onBeforeSend: (method, param) => {
          param.headers = {
            'Authorization': 'Bearer ' + this.token
          };
        }*/
      }),
      sort: [
        { selector: "Name", desc: false }
      ],
      paginate: true
    }

    this.allowModify = this.authService.HavePermission(71);
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

  fieldDataChanged(cellInfo, temp) {
    cellInfo.setValue(temp);
  }

  EditStart(e) {
    this.tempAddress = JSON.parse(JSON.stringify(e.data.address));
    this.rowIndex = this.dataGrid.instance.getRowIndexByKey(e.data.id);
    this.locationId = e.data.id;
    this.setStoreValue(e);
    this.notEditMode = true;
  }

  newRow(e) {
    this.tempAddress = { countryId: 190 };
    this.notEditMode = false;
  }

  onSaving(e) {
    if (!this.formAddress.instance.validate().isValid) {
      e.cancel = true;
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

  SavingStart(e) {
    this.rowIndex = 0;
  }

  onSaved(e) {
    this.defaultStoreForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/Stores',
      }),
      paginate: true
    }
  }

  setStoreValue(e) {
    this.dataGrid.instance.cellValue(this.rowIndex, "id", e.data.id);
    this.dataGrid.instance.cellValue(this.rowIndex, "defaultStoreId", null);

    this.defaultStoreForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/Stores',
      }),
      filter: ["plantLocationId", "=", this.locationId],
      paginate: true
    }
  }
}
