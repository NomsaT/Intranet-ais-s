import { DatePipe } from '@angular/common';
import { Component, Inject, Input, OnInit, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { error, log } from 'console';
import { DxDataGridComponent } from 'devextreme-angular';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import DataSource from 'devextreme/data/data_source';
import { exportDataGrid } from 'devextreme/excel_exporter';
import notify from 'devextreme/ui/notify';
import { Workbook } from 'exceljs';
import saveAs from 'file-saver';
import { map } from 'rxjs/operators';
import { currencyZAR } from 'src/globalConstants';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { StocktakeService } from '../../core/services/stocktake.service';
import { tab } from "../models/tab";

@Component({
  selector: 'app-stocktake-cycle',
  templateUrl: './stocktake-cycle.component.html',
  styleUrls: ['./stocktake-cycle.component.scss']
})
export class StocktakeCycleComponent {
  dataSource: any;
  StockForeignDataSource: any;
  DepartmentForeignDataSource: any;
  LocationForeignDataSource: any;
  StoreForeignDataSource: any;
  CurrencyForeignDataSource: any;
  cycleDataSource: any;
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  currentColor: any;
  isActive: string;
  baseUrl: any;
  pageName: string = "Stocktake Cyle";
  currencyZAR: any;
  event: any;
  CancelDownload = true;
  popupDownloadConfirmVisible = false;
  allowModify = false;
  allowView = false;
  allowHistory = false;
  popupScheduleVisible = false;
  popupCycleVisible = false;
  rowIndex = 0;

  tabs: tab[];
  breadcrumbs: any;

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
            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Stocktake-List.xlsx');
          });
      });
    }
  };
    showInfoIcon: boolean;
    dataDelete: any;
    stocktakeReportDataSource: any;
    isLoading: boolean;
    service: any;
    editRowKey: any;
    changes: any[];
  @Input() id: number;
  data: any;
    
  onToolbarPreparing(e): void {
    if (this.allowModify == true) {
      e.toolbarOptions.items.unshift({
        location: 'after',
        widget: 'dxButton',
        options: {
          icon: "plus",
          type: "success",
          useSubmitBehavior: true,
          
        }
      });
    }
  }

  @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;
  @ViewChild('content') content;
  router: any;
  oldCode: string;
  editingMode: boolean;
  stockTake: any;
  stockTakeId: any;
  popupVisible: boolean;
  allowReport = false;
  visible = false;
  disabled = false;
  disableStocktakeName = true;
  events: Array<string> = [];

  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal,
    private stocktakeService: StocktakeService, private globalMethodsService: GlobalMethodsService, private authService: AuthenticationService) {
    this.currencyZAR = currencyZAR;
    this.baseUrl = baseUrl;
    this.dataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/StocktakePeriod',
        insertUrl: baseUrl + 'api/StocktakePeriod/AddStocktakeCycle',
        updateUrl: baseUrl + 'api/StocktakePeriod/EditCycle',
        deleteUrl: baseUrl + 'api/StocktakePeriod/DeleteCycle',
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
    }

    this.allowModify = this.authService.HavePermission(112);
    this.allowView = this.authService.HavePermission(113);
    console.log(this.allowView);
    
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

  onHidden(e) {
    this.dataGrid.instance.getDataSource().reload();
    
  }

  onSavedParent(e) {
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() =>
      this.router.navigate(['/Stocktake'])
    );
  }

  editStart(e) {

    //reset the stocktake value
    this.disableStocktakeName = true;
    if (!e.data.stocktakeName.includes("Unscheduled")) {
      this.disableStocktakeName = false;
    }
    this.rowIndex = this.dataGrid.instance.getRowIndexByKey(e.data.id);
    this.visible = true;
    this.disabled = true;
  }

  SavingStart(e) {
    this.rowIndex = 0;
    this.disabled = false;
    this.visible = false;
  }

  onSaved(e) {
    
    this.stocktakeService.getCycle.next(1);
  }

  insertRow(e) {
    this.id = e.data.id;
  }
  
  onEditCanceled(e) {
    this.disabled = false;
    this.visible = false;
  }

  editorPreparing(e: any) {   
    if (e.parentType !== "dataRow")
      return;
    if (e.dataField === "startDate") {
      const datepipe: DatePipe = new DatePipe('en-US')
      let formattedDate = datepipe.transform(new Date().toLocaleString(), 'dd-MMM-YYYY')
      e.editorOptions.min = formattedDate;
    }
    if (e.dataField === "endDate") {
      
      const datepipe: DatePipe = new DatePipe('en-US')
      let formattedDate = datepipe.transform(new Date().toLocaleString(), 'dd-MMM-YYYY')
      e.editorOptions.min = formattedDate;
    }
  }
}
