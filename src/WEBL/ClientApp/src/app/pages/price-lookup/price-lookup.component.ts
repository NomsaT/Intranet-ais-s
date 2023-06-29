import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DxDataGridComponent, DxFormComponent } from 'devextreme-angular';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import { exportDataGrid } from 'devextreme/excel_exporter';
import notify from 'devextreme/ui/notify';
import { Workbook } from 'exceljs';
import saveAs from 'file-saver';
import { currencyZAR } from 'src/globalConstants';
import { priceIncrease } from '../../core/models/priceIncrease.models';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { PriceIncreaseService } from '../../core/services/priceIncrease.service';
import { PriceLookUpService } from '../../core/services/pricelookup.service';
import { tab } from "../models/tab";
import { popupAutoSelectService } from '../../core/services/popupAutoSelect.service';
import { StockService } from '../../core/services/stock.service';
import { User } from 'src/app/core/models/user.model';
import CustomStore from 'devextreme/data/custom_store';
import { formatDate } from 'devextreme/localization';

interface Values {
  username: string;
  currency: string;
  price: number;
  supplierId: number;
  stockId: number;
  date: Date;
  comment: string
}

@Component({
  selector: 'app-price-lookup',
  templateUrl: './price-lookup.component.html',
  styleUrls: ['./price-lookup.component.scss']
})
export class PriceLookUpComponent implements OnInit {
  priceIncrease: priceIncrease = new priceIncrease();
  tabs: tab[];
  dataSource: any;
  HistoryDataSource: any;
  CurrentDataSource: any;
  breadcrumbs: any;
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  SupplierForeignDataSource: any;
  StockForeignDataSource: any;
  StockUnassignedDataSource: any;
  IncreaseTypeForeignDataSource: any;
  CurrencyForeignDataSource: any;
  defaultVisible = false;
  showInfoIcon = false;
  popupVisible = false;
  popupVisibleSupplier = false;
  popupVisibleUpdate = false;
  baseUrl: any;
  priceIncreasePrimaryKey = 0;
  priceLookUpPrimaryKey = 0;
  showAddbtn = true;
  showEditbtn = false;
  stockId: any;
  pageName: string = "Stock Item Price";
  priceIncreaseReminderFormInstance: any;
  unassignedStockId = 0;
  currencyZAR: any;
  event: any;
  CancelDownload = true;
  popupDownloadConfirmVisible = false;
  popupDownloadConfirmHistoryVisible = false;
  allowModify = false;
  allowReminderSet = false;
  allowReminderAdd = false;
  allowHistory = false;
  allowView = false;
  supplierId = null;
  rowIndex = 0;
  currency = "ZAR";

  //#region Editor Options
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
            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Current-Prices-List.xlsx');
          });
      });
    }
  };

  noDownloadHistoryButtonOptions: any = {
    text: "No",
    onClick: (e) => {
      this.popupDownloadConfirmHistoryVisible = false;
      this.CancelDownload = true;

    }
  };

  yesDownloadHistoryButtonOptions = {
    text: "Yes",
    onClick: () => {
      this.popupDownloadConfirmHistoryVisible = false;

      const workbook = new Workbook();
      const worksheet = workbook.addWorksheet('Main sheet');
      exportDataGrid({
        component: this.event.component,
        worksheet: worksheet
      }).then(function () {
        workbook.xlsx.writeBuffer()
          .then(function (buffer: BlobPart) {
            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Price-History-List.xlsx');
          });
      });
    }
  };

  buttonAdd: any = {
    icon: "check",
    text: "Add Reminder",
    type: "default",
    useSubmitBehavior: true,
    onClick: () => {
      if (this.priceIncreaseReminderFormInstance.validate().isValid) {
        this.onVoidAddIncrease();
      }
    }
  }

  buttonEdit: any = {
    icon: "edit",
    text: "Update Reminder",
    type: "default",
    useSubmitBehavior: true,
    onClick: () => {
      if (this.priceIncreaseReminderFormInstance.validate().isValid) {
        this.onVoidEditIncrease();
      }
    }
  }

  buttonRemove: any = {
    icon: "trash",
    text: "Remove Reminder",
    type: "danger",
    useSubmitBehavior: true,
    onClick: () => {
      this.onVoidRemoveIncrease();
    }
  }

  buttonOptions: any = {
    icon: "plus",
    type: "default",
    hint: "Add a new supplier",
    useSubmitBehavior: false,
    onClick: () => {
      this.popupVisibleSupplier = true;
    }
  }

  noUpdateButton: any = {
    text: "No",
    onClick: () => {
      this.popupVisibleUpdate = false;
    }
  };

  yesUpdateButton: any = {
    text: "Yes",
    onClick: () => {
      this.onVoidUpdateIncrease();
      this.popupVisibleUpdate = false;
    }
  };
  //#endregion

  @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;
  @ViewChild("priceIncreaseReminderForm", { static: false }) addStockQuantityForm: DxFormComponent
  @ViewChild('content') content;
  username: string;
  requests: string[] = [];
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private priceIncreaseService: PriceIncreaseService, private autoSelect: popupAutoSelectService, private stockService: StockService,
    private pricelookupService: PriceLookUpService, private globalMethodsService: GlobalMethodsService, private router: Router, private route: ActivatedRoute, private authService: AuthenticationService) {
    this.currencyZAR = currencyZAR;
    this.breadcrumbs = "Current Prices";
    this.baseUrl = baseUrl;
    this.rowIndex = 0;
    this.username = this.authService.currentUser().username;
    this.tabs = [
      { text: "Current Prices" },
      { text: "Price History" }
    ];
    this.setStockValue = this.setStockValue.bind(this);
    this.route.queryParamMap.subscribe(queryParams => {
      this.stockId = queryParams.get('stockId');
    });

    this.CurrentDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/PriceLookUp',
        updateUrl: baseUrl + 'api/PriceLookUp',
        insertUrl: baseUrl + 'api/PriceLookUp',
        deleteUrl: baseUrl + 'api/PriceLookUp',
        onInserted: () => {
          this.globalMethodsService.ToastInsertSuccessMessage(this.pageName);
        },
        onUpdated: () => {
          this.globalMethodsService.ToastUpdateSuccessMessage(this.pageName);
        },
        onRemoved: () => {
          this.globalMethodsService.ToastDeleteSuccessMessage(this.pageName);
        },
        onBeforeSend: (method, param) => {
          if (method == 'load') return;
          if (param.data.values == null) {
            param.data.values = JSON.stringify({ username: this.username });
          } else {
            const data: Values = JSON.parse(param?.data?.values);
            data.username = this.username;
            param.data.values = JSON.stringify(data);
          }
        }
      }),
      paginate: true,
      sort: ([
        { selector: "productName", desc: false }
      ])
    }

    this.SupplierForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Supplier',
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

    this.StockUnassignedDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Stock/getUnassignedStock?StockId=' + this.unassignedStockId,
      }),
      sort: [
        { selector: "InternalProductNameFull", desc: false }
      ],
      paginate: true
    }


    this.HistoryDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/PriceLookUpLog',
        updateUrl: baseUrl + 'api/PriceLookUpLog',
        insertUrl: baseUrl + 'api/PriceLookUpLog',
        deleteUrl: baseUrl + 'api/PriceLookUpLog',
        /*onBeforeSend: (method, param) => {
          param.headers = {
            'Authorization': 'Bearer ' + this.authService.getToken()
          };
        }*/
      }),
      paginate: true,
      sort: ([
        { selector: "productName", desc: false }
      ])
    }

    this.IncreaseTypeForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/IncreaseType',
      }),
      paginate: true
    }

    this.CurrencyForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Currency',
      }),
      paginate: true
    }

    this.allowModify = this.authService.HavePermission(19);
    this.allowView = this.authService.HavePermission(18);
    this.allowReminderSet = this.authService.HavePermission(21);
    this.allowReminderAdd = this.authService.HavePermission(22);
    this.allowHistory = this.authService.HavePermission(20);
  }
  sendRequest(url: string, method = 'GET', data: any = {}): any {
    this.logRequest(method, url, data);

    const httpParams = new HttpParams({ fromObject: data });
    const httpOptions = { withCredentials: true, body: httpParams };
    let result;

    switch (method) {
      case 'GET':
        result = this.http.get(url, httpOptions);
        break;
      case 'PUT':
        result = this.http.put(url, httpParams, httpOptions);
        break;
      case 'POST':
        result = this.http.post(url, httpParams, httpOptions);
        break;
      case 'DELETE':
        result = this.http.delete(url, httpOptions);
        break;
    }
    return result;
  }

  logRequest(method: string, url: string, data: object): void {
    const args = Object.keys(data || {}).map((key) => `${key}=${data[key]}`).join(' ');

    const time = formatDate(new Date(), 'HH:mm:ss');

    this.requests.unshift([time, method, url.slice(this.baseUrl.length), args].join(' '));
  }

  savePriceIncreaseReminderFormInstance(e) {
    this.priceIncreaseReminderFormInstance = e.component;
  }
  /*
    randFormatConversion(cellInfo) {
      if (cellInfo.value)
        return GlobalMethodsService.formatCurrency(cellInfo.value, "R");
      else
        return cellInfo.value;
    }*/

  getStock(rowData) {
    if (rowData) {
      return rowData.code + " - " + rowData.internalProductName;
    } else {
      return "";
    }
  }

  ngOnInit() {

    this.username = this.authService.currentUser().username;


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

  onOptionChanged(e) {

    if (e != null || e != undefined || e != 0) {
      if (e.addedItems[0].text == "Price History" || e.addedItems[0].text == "Current Prices") {
        this.breadcrumbs = e.addedItems[0].text;
      } else {
        this.breadcrumbs = "";
      }

      if (e.addedItems[0].text == "Price History") {
        this.showInfoIcon = true;
        this.HistoryDataSource = {
          store: AspNetData.createStore({
            key: 'id',
            loadUrl: this.baseUrl + 'api/PriceLookUpLog',
          }),
          paginate: true,
          sort: ([
            { selector: "productName", desc: false }
          ])
        }
      } else {
        this.showInfoIcon = false;
      }
    }
  }

  onCellPrepared(e) {
    if (e.rowType === "data" && e.column.caption != "Add Increase" && e.columnIndex != 7) {

      if (e.data.priceIncreaseCount > 0) {
        e.cellElement.style.color = "#fff";
        e.cellElement.style.backgroundColor = "#3379b7";
      }

      if (e.data.priceIncreaseDateCount > 0) {
        e.cellElement.style.color = "#fff";
        e.cellElement.style.backgroundColor = "#d9534f";
      }
    }

    if (e.rowType === "data" && e.column.caption == "Add Increase") {
      e.cellElement.style.color = "#fff";
      e.cellElement.style.backgroundColor = "#5cb85c";
      if (e.data.priceIncreaseCount == 0) {
        e.cellElement.style.visibility = "hidden";
      }
    }
  }

  toggleDefault() {
    this.defaultVisible = !this.defaultVisible;
  }

  priceincrease(e) {
    this.priceIncrease = new priceIncrease();

    switch (e.column.caption) {
      case "Price Increase Reminder": {
        this.popupVisible = true;
        //reset values
        this.priceIncrease.priceLookUpId = e.data.id;
        this.priceIncrease.username = this.authService.currentUser().username;
        this.priceLookUpPrimaryKey = e.data.id;
        this.priceIncreasePrimaryKey = 0;
        /////////////

        this.priceIncreaseService.getItemInfo(e.data.id).subscribe(data => {
          this.dataSource = data;
          if (data != "") {
            this.priceIncreasePrimaryKey = data[0].id;
            this.priceIncrease.increaseTypeId = data[0].increaseTypeId;
            this.priceIncrease.increase = data[0].increase;
            this.priceIncrease.date = data[0].date;
            this.priceIncrease.comment = data[0].comment;
            this.showAddbtn = false;
            this.showEditbtn = true;
          } else {
            this.showAddbtn = true;
            this.showEditbtn = false;
          }
        });
        break;
      }
      case "Add Increase": {
        //reset values
        this.priceIncrease.priceLookUpId = e.data.id;
        this.priceIncrease.username = this.authService.currentUser().username;
        this.priceLookUpPrimaryKey = e.data.id;
        this.priceIncreasePrimaryKey = 0;
        /////////////
        this.popupVisibleUpdate = true;
        break;
      }
    }
  }

  onVoidAddIncrease() {
    this.priceIncrease.username = this.authService.currentUser().username;

    this.priceIncreaseService.postPriceIncrease(this.priceIncrease).subscribe(data => {
      if (data != "") {
        notify("Item flagged for price increase", 'success', 5000);
        this.popupVisible = false;
        this.priceIncrease = new priceIncrease();
        this.dataGrid.instance.getDataSource().reload();
        this.globalMethodsService.priceLookBadgeRefresh(true);
      } else {
        notify("Item not flagged for price increase", 'error', 5000);
      }
    }, subError => {
      notify(subError.error, 'error', 5000);
    });
  }

  onVoidUpdateIncrease() {
    this.priceIncreaseService.getItemInfo(this.priceLookUpPrimaryKey).subscribe(data => {
      this.dataSource = data;
      if (data != "") {
        this.priceIncreasePrimaryKey = data[0].id;
        this.priceIncrease.increaseTypeId = data[0].increaseTypeId;
        this.priceIncrease.increase = data[0].increase;
        this.priceIncrease.date = data[0].date;
        this.priceIncrease.comment = data[0].comment;
        this.priceIncrease.username = this.authService.currentUser().username;


        this.pricelookupService.putPriceLookUp(this.priceIncrease).subscribe(data => {
          if (data != "") {
            notify("Price Increase added to item.", 'success', 5000);
            this.popupVisible = false;
            this.priceIncrease = new priceIncrease();
            this.dataGrid.instance.getDataSource().reload();
            this.globalMethodsService.priceLookBadgeRefresh(true);
            this.priceLookUpPrimaryKey = 0;
          } else {
            notify("Price Increase not added to item.", 'error', 5000);
          }
        }, subError => {
          notify(subError.error, 'error', 5000);
        });

      } else {
        notify("Item does not have a price increase reminder.", 'error', 5000);
      }
    });
  }

  onVoidRemoveIncrease() {
    if (this.priceIncreasePrimaryKey != 0) {
      this.priceIncreaseService.removeItemInfo(this.priceIncreasePrimaryKey).subscribe(data => {
        if (data) {
          notify("Price Increase Removed.", 'success', 5000);
          this.popupVisible = false;
          this.priceIncrease = new priceIncrease();
          this.dataGrid.instance.getDataSource().reload();
          this.globalMethodsService.priceLookBadgeRefresh(true);
        } else {
          notify("Price Increase not Deleted.", 'error', 5000);
        }
      });
    }
  }

  onVoidEditIncrease() {
    this.priceIncrease.username = this.authService.currentUser().username;
    this.priceIncreaseService.putPriceIncrease(this.priceIncrease).subscribe(data => {
      if (data != "") {
        notify("Item Updated.", 'success', 5000);
        this.popupVisible = false;
        this.priceIncrease = new priceIncrease();
        this.dataGrid.instance.getDataSource().reload();
        this.globalMethodsService.priceLookBadgeRefresh(true);
      } else {
        notify("Item Not Updated.", 'error', 5000);
      }
    }, subError => {
      notify(subError.error, 'error', 5000);
    });
  }

  onContentReady(e) {
    if (this.stockId != null) {
      this.filter();
    }
  }

  filter() {
    if (this.stockId != null) {
      this.dataGrid.instance.addRow();
    }
  }

  newRow(e) {
    this.unassignedStockId = 0;
    this.StockUnassignedDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/Stock/getUnassignedStock?StockId=' + this.unassignedStockId,
      }),
      paginate: true
    }

    e.data.price = 0;
    this.currency = "ZAR";
    if (this.stockId != null) {
      e.data.stockId = this.stockId;
      this.stockId = null;
    }
  }

  onEditCanceling(e) {
    this.stockId = null;
    this.unassignedStockId = 0;
  }

  onEditorPreparing(e) {
    if (e.parentType == "dataRow" && e.dataField == "date") {
      e.editorOptions.invalidDateMessage = "The date must have the following format: yyyy/MM/dd";
    }
  }

  onSaved(e) {
    this.stockId = null;
    this.unassignedStockId = 0;
  }

  onHidden(e) {
    this.dataGrid.instance.getDataSource().reload();
    this.autoSelect.GetNewestSupplier().subscribe(data => {
      this.dataGrid.instance.cellValue(this.rowIndex, "supplierId", data);
    }, subError => {
      notify(subError.error, 'error', 5000);
    });
  }

  EditStart(e) {
    this.priceIncrease.username = this.username;
    this.rowIndex = this.dataGrid.instance.getRowIndexByKey(e.data.id);
    this.supplierId = this.dataGrid.instance.cellValue(this.rowIndex, "supplierId");
    this.stockService.GetCurrency(this.supplierId).subscribe(data => {
      if (data != null) {
        this.dataGrid.instance.cellValue(this.rowIndex, "currency", data["iso"]);
        this.currency = data["iso"];
      }
    }, subError => {
      notify(subError.error, 'error', 5000);
    });
    if (e.data.stockId) {
      this.unassignedStockId = e.data.stockId;
      this.StockUnassignedDataSource = {
        store: AspNetData.createStore({
          key: 'id',
          loadUrl: this.baseUrl + 'api/Stock/getUnassignedStock?StockId=' + this.unassignedStockId,
        }),
        filter: ["supplierId", "=", this.supplierId],
        paginate: true
      }
    } else {
      this.unassignedStockId = 0;
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

  onExportingHistory(e) {
    this.event = e;
    this.popupDownloadConfirmHistoryVisible = true;
    if (this.CancelDownload) {
      e.cancel = true;
    }
  }

  setStockValue(e) {
    this.supplierId = e.value;
    this.dataGrid.instance.cellValue(this.rowIndex, "supplierId", e.value);
    this.dataGrid.instance.cellValue(this.rowIndex, "stockId", null);
    this.stockService.GetCurrency(e.value).subscribe(data => {
      if (data != null) {
        this.dataGrid.instance.cellValue(this.rowIndex, "currency", data["iso"]);
        this.currency = data["iso"];
      }
    }, subError => {
      notify(subError.error, 'error', 5000);
    });
    console.log(this.unassignedStockId);

    this.StockUnassignedDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/Stock/getUnassignedStock?StockId=' + this.unassignedStockId,
      }),
      filter: ["supplierId", "=", this.supplierId],
      paginate: true
    }
  }

  onExportedHistory(e) {
    notify("Successfully Downloaded", 'success', 5000);
  }

  oldPriceISO(rowData) {
    return rowData.currency + " " + GlobalMethodsService.formatCurrency(rowData.oldPrice, "");
  }

  newPriceISO(rowData) {
    return rowData.currency + " " + GlobalMethodsService.formatCurrency(rowData.newPrice, "");
  }

  priceISO(rowData) {
    return rowData.currency + " " + GlobalMethodsService.formatCurrency(rowData.price, "");
  }
}
