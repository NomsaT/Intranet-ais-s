import { AfterViewInit, Component, Inject, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import { exportDataGrid } from 'devextreme/excel_exporter';
import notify from 'devextreme/ui/notify';
import { Workbook } from 'exceljs';
import saveAs from 'file-saver';
import { currencyZAR, EnumRecipe } from 'src/globalConstants';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { stockQuantity } from '../../core/models/stockQuantity.models';
import { tab } from "../models/tab";
import { DxDataGridComponent, DxFormComponent } from 'devextreme-angular';
import { StockService } from '../../core/services/stock.service';

@Component({
  selector: 'app-production-stock',
  templateUrl: './production-stock.component.html',
  styleUrls: ['./production-stock.component.scss']
})
export class ProductionStockComponent implements AfterViewInit {
  dataSource: any;
  UnitofMeasureForeignDataSource: any;
  StockCategoryForeignDataSource: any;
  DepartmentForeignDataSource: any;
  LocationForeignDataSource: any;
  StoreForeignDataSource: any;
  StoreTypeForeignDataSource: any;
  StockGroupForeignDataSource: any;
  CurrencyForeignDataSource: any;
  StorageTypeForeignDataSource: any;
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  currencyZAR: any;
  event: any;
  CancelDownload = true;
  popupDownloadConfirmVisible = false;
  allowManage = false;
  CodeAndName: any;
  popupManageStockVisible = false;
  tabs: tab[];
  stockQuantity: stockQuantity = new stockQuantity();
  managestockinfo = "";
  option: any;
  removeStockQuantityFormInstance: any;
  validationFails = [];
  popupUomQ = 0;
  popupUom2Q = 0;
  popupPackSize = 0;
  uom: any;
  code: any;
  productName: any;
  stockId: any;
  show = false;
  popupEstimatedPacks = 0;
  UomRemoveForeignDataSource: any;
  showCustomMixTab = false;
  baseUrl: any;
  MainStockId: any;
  itemMixingDataSource: any;
  rowIndex = 0;
  isRecipe = false;

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
            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'location-stock-List.xlsx');
          });
      });

    }
  };

  buttonCancel: any = {
    text: "Cancel",
    type: "danger",
    useSubmitBehavior: true,
    onClick: () => {
      this.onVoidCancel();
    }
  }

  buttonOk: any = {
    text: "OK",
    type: "success",
    useSubmitBehavior: true,
    onClick: ($event) => {
      switch (this.option) {
        case "remove": {
          this.validateForms(this.option);
          if (this.validationFails.length == 0) {
            this.onVoidOk($event);
            this.popupManageStockVisible = false;
          }
          break;
        }
      }
    }
  }

  @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;
  @ViewChild("removeStockQuantityForm", { static: false }) removeStockQuantityForm: DxFormComponent;
  @ViewChild('content') content;
  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private stockService: StockService, private globalMethodsService: GlobalMethodsService, private authService: AuthenticationService) {
    this.baseUrl = baseUrl;
    this.currencyZAR = currencyZAR;
    this.buildtabs(this.showCustomMixTab);
    this.rowIndex = 0;
    this.isRecipe = false;

    this.dataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/ProductionStock'
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

    this.UnitofMeasureForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/UnitOfMeasurement',
      }),
      paginate: true
    }

    this.StockCategoryForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/StockCategory',
      }),
      paginate: true
    }

    this.DepartmentForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Department',
        /* onBeforeSend: (method, param) => {
           param.headers = {
             'Authorization': 'Bearer ' + this.token
           };
         }*/
      }),
      paginate: true
    }

    this.LocationForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/PlantLocation',
        /* onBeforeSend: (method, param) => {
           param.headers = {
             'Authorization': 'Bearer ' + this.token
           };
         }*/
      }),
      paginate: true
    }

    this.StoreForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Stores',
        /* onBeforeSend: (method, param) => {
           param.headers = {
             'Authorization': 'Bearer ' + this.token
           };
         }*/
      }),
      paginate: true
    }

    this.StoreTypeForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/StoreTypes',
        /* onBeforeSend: (method, param) => {
           param.headers = {
             'Authorization': 'Bearer ' + this.token
           };
         }*/
      }),
      paginate: true
    }

    this.StockGroupForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/StockGroup',
        /* onBeforeSend: (method, param) => {
           param.headers = {
             'Authorization': 'Bearer ' + this.token
           };
         }*/
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

    this.StorageTypeForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/StorageType',
      }),
      paginate: true
    }

    this.allowManage = this.authService.HavePermission(116);
  }

  randFormatConversion(cellInfo) {
    if (cellInfo.value)
      return GlobalMethodsService.formatCurrency(cellInfo.value, "R");
    else
      return cellInfo.value;
  }

  buildtabs(visible) {
    if (visible) {
      this.tabs = [
        { text: "Remove Stock" }
      ];
    } else {
      this.tabs = [
        { text: "Remove Stock" }
      ];
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

  initialUomQuantity(rowData) {
    return rowData.quantity + " (" + rowData.uomName + ")";
  }

  priceISO(rowData) {
    return rowData.supplierCurrencyIso + " " + GlobalMethodsService.formatCurrency(rowData.price, "") ;
  }

  saveRemoveStockQuantityFormInstance(e) {
    this.removeStockQuantityFormInstance = e.component;
  }

  onOptionChanged(e) {
    if (e) {
      this.stockQuantity.itemQuantity = null;
      switch (e.addedItems[0].text) {
        case "Remove Stock": {
          this.option = "remove";
          this.managestockinfo = "Enter the total UoM you want to remove below";
          break;
        }
        default: {
          this.option = "";
          break;
        }
      }
    }
  }

  validateForms(option) {
    this.validationFails = [];

    switch (option) {
      case "remove": {
        const removeStockQuantityValidation = this.removeStockQuantityFormInstance.validate();
        this.validationFails = this.validationFails.concat(removeStockQuantityValidation.brokenRules.map(function (rule) {
          return rule.message
        }));
        break;
      }
    }
  }

  manageStockOnClick(e) {
    if (e.rowType === "data" && e.column.caption == "Consume Stock") {
      this.stockId = e.data.id;
      this.CodeAndName = e.data.code + " - " + e.data.productName;
      this.popupUomQ = e.data.quantity + e.data.uomName;
      if (e.data.secondQuantity == 0) {
        this.show = false;
      } else {
        this.popupUom2Q = e.data.secondQuantity + e.data.secondUomName;
        this.show = true;
      }
      this.popupPackSize = e.data.packSize;

      this.popupEstimatedPacks = e.data.quantity / e.data.packQuantity;

      this.UomRemoveForeignDataSource = {
        store: AspNetData.createStore({
          key: 'id',
          loadUrl: this.baseUrl + 'api/UnitOfMeasurement/getUOMbyStockConsume?itemStockId=' + this.stockId,
        }),
        paginate: true
      }

      this.showCustomMixTab = false;
      if (e.data.stockCategoryId == EnumRecipe) {
        this.showCustomMixTab = true;
        this.MainStockId = e.data.id;
        this.stockService.GetMixRecipe(this.MainStockId).subscribe(data => {
          if (data != null && data != undefined) {
            this.itemMixingDataSource = data;
          }
        }, subError => {
          notify(subError.error, 'error', 5000);
        });
      }

      this.buildtabs(this.showCustomMixTab);
      this.popupManageStockVisible = true;
    }
  }

  onVoidCancel() {
    this.stockQuantity = new stockQuantity();
    this.popupManageStockVisible = false;
    this.rowIndex = 0;
    this.isRecipe = false;
  }

  onVoidOk(e) {
    switch (this.option) {
      case "remove": {
        this.stockQuantity.stockId = this.stockId;
        this.stockService.RemoveConsumeStock(this.stockQuantity).subscribe(data => {
          if (data !== "") {
            this.stockQuantity = new stockQuantity();
            this.popupManageStockVisible = false;

            this.dataGrid.instance.getDataSource().reload();

            notify("Quantity Removed.", 'success', 5000);
          } else {
            notify("Quantity Not Removed.", 'error', 5000);
          }
        }, subError => {
          notify(subError.error, 'error', 5000);
        });
        break;
      }
      default: {
        notify("The action can't be applied.", 'error', 5000);
        break;
      }
    }
  }
}
