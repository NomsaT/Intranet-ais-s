import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { error, log } from 'console';
import { DxDataGridComponent } from 'devextreme-angular';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import { exportDataGrid } from 'devextreme/excel_exporter';
import notify from 'devextreme/ui/notify';
import { Workbook } from 'exceljs';
import saveAs from 'file-saver';
import { map, refCount, retry } from 'rxjs/operators';
import { Stocktake } from 'src/app/core/models/stocktake.model';
import { User } from 'src/app/core/models/user.model';
import { currencyZAR } from 'src/globalConstants';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { StocktakeService } from '../../core/services/stocktake.service';
import { tab } from "../models/tab";

@Component({
  selector: 'app-stocktake',
  templateUrl: './stocktake.component.html',
  styleUrls: ['./stocktake.component.scss']
})
export class StocktakeComponent {
  dataSource: any;
  StockForeignDataSource: any;
  DepartmentForeignDataSource: any;
  LocationForeignDataSource: any;
  StoreForeignDataSource: any;
  CurrencyForeignDataSource: any;
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  currentColor: any;
  isActive: string;
  baseUrl: any;
  pageName: string = "Stock Item";
  currencyZAR: any;
  event: any;
  CancelDownload = true;
  popupDownloadConfirmVisible = false;
  allowModify = false;
  allowView = false;
  allowHistory = false;
  popupScheduleVisible = false;
  popupCycleVisible = false;
  popupRecountVisible = false;
  tabs: tab[];
  breadcrumbs = "Current Stocktake";

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
  stocktakePeriodDataSource: any;
  stocktakeCycleDataSource: any;
  allowViewCycle = false;
  userInstance: any;
  userDataSource: any;
  stocktakeModel: Stocktake;

  onToolbarPreparing(e): void {
    if (this.allowModify == true && this.allowViewCycle == true) {
      e.toolbarOptions.items.unshift({
        location: 'after',
        widget: 'dxButton',
        options: {
          text: 'Start/Schedule Stocktake',
          type: "success",
          useSubmitBehavior: false,
          onClick: () => {
            this.popupScheduleVisible = true;
          }
        }
      });
    }
  }
  @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;
  @ViewChild('content') content;
  rowIndex: number;
  router: any;
  oldCode: string;
  editingMode: boolean;

  stockTake: any;
  stockTakeId: number;
  popupVisible: boolean;
  //HistoryDataSource: any;
  dataSourceHistory: any;
  allowReport = false;
  user: User;

  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal,
    private stocktakeService: StocktakeService, private globalMethodsService: GlobalMethodsService, private authService: AuthenticationService) {
    this.currencyZAR = currencyZAR;
    this.baseUrl = baseUrl;
    this.tabs = [
      { text: "Current Stocktake" },
      { text: "Stocktake Report" },
      { text: "Stocktake History" }
    ];

    this.getLog();

    this.dataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Stocktake',

      }),
      paginate: true,
    }
    this.StoreForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Stores',
      }),
      paginate: true
    }
    this.StockForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Stock',
      }),
      paginate: true
    }
    this.LocationForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/PlantLocation',
      }),
      paginate: true
    }
    this.stocktakeReportDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/StocktakeReport',
      }),
      paginate: true
    }
    this.stocktakePeriodDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/StocktakePeriod',
      }),
      paginate: true
    }

    this.dataSourceHistory = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/Stocktake/GetStocktakeLog',
      }),
      paginate: true,
    }

    this.userDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/FullEmployeeName',
      }),
      paginate: true,
    }

    // this.stocktakeCycleDataSource = {
    //   store: AspNetData.createStore({
    //     key: 'id',
    //     loadUrl: this.baseUrl + 'api/StocktakePeriod',
    //   }),
    //   paginate: true,
    // }

    this.allowModify = this.authService.HavePermission(107);
    this.allowView = this.authService.HavePermission(106);
    this.allowViewCycle = this.authService.HavePermission(113);
    this.allowHistory = this.authService.HavePermission(108);
    this.allowReport = this.authService.HavePermission(111);
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
  getStock(rowData) {
    if (rowData) {
      return rowData.code + " - " + rowData.internalProductName;
    } else {
      return "";
    }
  }

  buttonRemove: any = {
    text: "Cancel",
    useSubmitBehavior: true,
    onClick: () => {
      this.popupRecountVisible = false;
    }
  }

  buttonAdd: any = {
    text: "Save",
    useSubmitBehavior: true,
    onClick: () => {
      if (this.userInstance.validate().isValid) {
        this.stocktakeService.recountStocktake(this.stocktakeModel.id, this.user.id).subscribe(data => {
          this.getLog();
          notify("Recounted Successfully", 'success', 2000);
          this.dataGrid.instance.refresh();
        }, error => {
          this.dataGrid.instance.refresh();
          notify(error.error, 'error', 5000);
        });
        this.popupRecountVisible = false;
      }
    }
  }

  userFormInstance(e) {
    this.userInstance = e.component;
  }

  stocktake(e) {
    if (e.column.caption == "Recount") {
      this.popupRecountVisible = true;
      this.stocktakeModel = e.data;
      return;
    }
    else if (e.column.caption == "Approve") {
      this.stocktakeService.approveStocktake(e.data.id).subscribe(data => {
        this.getLog();
        this.getStocktakeReport();

        notify("Stock Approved", 'success', 2000);
        this.getStocktakePeriod();
        this.dataGrid.instance.refresh();
      }, error => {
        this.dataGrid.instance.refresh();
        this.getLog();
        notify(error.error, 'info', 5000);
      })
    }
  }

  getLog() {
    this.stocktakeService.getLog()
      .pipe((map(s => {
        return s.map(x => {
          return {
            id: x.id,
            countQty: x.countQty,
            currentQty: x.currentQty,
            plantLocationName: x.plantLocationName,
            recount: x.recount,
            recountDate: x.recountDate == "1900-01-01T00:00:00" ? "" : x.recountDate,
            stockTakeDate: x.stockTakeDate,
            approveDate: x.approveDate == "1900-01-01T00:00:00" ? "" : x.approveDate,
            stockFullName: x.stockFullName,
            storeName: x.storeName,
            actions: x.actions,
            userFullName: x.userFullName
          }
        })

      })))
      .subscribe(log => {
        this.dataSourceHistory = log;
      }, error => {
        notify(error.error, 'error', 2000);

      });
  }

  getStocktake() {
    this.stocktakeService.getStocktake().subscribe(data => {
      this.dataSource = data;
      this.dataGrid.instance.getDataSource().reload();
    });
  }
  getStocktakeReport() {
    this.stocktakeService.getStocktakeReport().subscribe(data => {
      this.stocktakeReportDataSource = data;
    }, error => {
      notify(error.error, 'error', 2000);

    });
  }
  getStocktakePeriod() {
    this.stocktakeService.getStocktakePeriod().subscribe(data => {
      this.stocktakeCycleDataSource = data;
    }, error => {
      notify(error.error, 'error', 2000);

    });
  }
  onCellPrepared(e) {
    if (e.rowType === "data") {

      if (e.data.recount == true) {
        e.cellElement.style.color = "#fff";
        e.cellElement.style.backgroundColor = "#8ee4e7";
      }
    }

  }

  onOptionChanged(e) {

    if (e != null || e != undefined || e != 0) {
      if (e.addedItems[0].text == "Current Stocktake" || e.addedItems[0].text == "Stocktake Report" || e.addedItems[0].text == "Stocktake History") {
        this.breadcrumbs = e.addedItems[0].text;
      } else {
        this.breadcrumbs = "";
      }
    }
  }

  onHidden(e) {
    this.dataGrid.instance.getDataSource().reload();
  }
}
