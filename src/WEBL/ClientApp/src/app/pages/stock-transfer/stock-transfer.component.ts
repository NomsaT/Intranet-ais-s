import { AfterViewInit, Component, Inject, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DxDataGridComponent, DxFormComponent } from 'devextreme-angular';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import { exportDataGrid } from 'devextreme/excel_exporter';
import notify from 'devextreme/ui/notify';
import { Workbook } from 'exceljs';
import saveAs from 'file-saver';
import { currencyZAR } from 'src/globalConstants';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { StockService } from '../../core/services/stock.service';
import { stockTransferLocation } from '../../core/models/stockTransferLocation.models';
import { stockTransferDepartment } from '../../core/models/stockTransferDepartment.models';

@Component({
  selector: 'app-stock-transfer',
  templateUrl: './stock-transfer.component.html',
  styleUrls: ['./stock-transfer.component.scss']
})
export class StockTransferComponent implements AfterViewInit {
  dataSource: any;
  UnitofMeasureForeignDataSource: any;
  StockCategoryForeignDataSource: any;
  DepartmentForeignDataSource: any;
  FilteredDepartmentForeignDataSource: any;
  LocationForeignDataSource: any;
  StoreForeignDataSource: any;
  StoreTypeForeignDataSource: any;
  NewStoreTypeForeignDataSource: any;
  StockGroupForeignDataSource: any;
  StockForeignDataSource: any;
  FilteredLocationForeignDataSource: any;
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
  allowModify = false;
  LocationId = null;
  NewLocationId = null;
  baseUrl: any;
  popupTransferStockVisible = false;
  popupTransferStockDepVisible = false;
  addTransferStockFormInstance: any;
  addTransferStockDepFormInstance: any;
  stockTransferLocation: stockTransferLocation = new stockTransferLocation();
  stockTransferDepartment: stockTransferDepartment = new stockTransferDepartment();
  validationFails = [];
  validationFailsDep = [];
  stockInfo: any;
  code: any;
  productName: any;
  uomName: any;
  quantity: any;
  unitQty: any;
  packSize: any;
  packQty: any;
  StockId: number;
  priorities: string[];
  UomRemoveForeignDataSource: any;
  totalLocation: any;
  totalStore: any;
  totalDep: any;

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
    StoreId: any;

  onToolbarPreparing(e): void {
    if (this.allowModify == true) {
      e.toolbarOptions.items.unshift({
        location: 'after',
        widget: 'dxButton',
        options: {
          icon: 'cart',
          text: 'Transfer Stock by Location',
          type: "normal",
          useSubmitBehavior: false,
          onClick: () => {
            this.popupTransferStockVisible = true;
          }
        }
      });

      e.toolbarOptions.items.unshift({
        location: 'after',
        widget: 'dxButton',
        options: {
          icon: 'cart',
          text: 'Transfer Stock by Department',
          type: "normal",
          useSubmitBehavior: false,
          onClick: () => {
            this.popupTransferStockDepVisible = true;
          }
        }
      });
    }
  }

  buttonCancel: any = {
    text: "Cancel",
    type: "danger",
    useSubmitBehavior: false,
    onClick: () => {
      this.onVoidCancel();
    }
  }

  buttonOk: any = {
    text: "OK",
    type: "success",
    useSubmitBehavior: true,
    onClick: ($event) => {
      this.validateForms();
      if (this.validationFails.length == 0) {
        if (this.stockTransferLocation.packQuantityMove != 0) {
          this.onVoidOk($event);
        } else {
          this.stockTransferLocation.packQuantityMove = null;
          notify("The Pack Quantity can't be 0.", 'error', 5000);
        }
      }
    }
  }

  buttonCancelDep: any = {
    text: "Cancel",
    type: "danger",
    useSubmitBehavior: false,
    onClick: () => {
      this.onVoidCancelDep();
    }
  }

  buttonOkDep: any = {
    text: "OK",
    type: "success",
    useSubmitBehavior: true,
    onClick: ($event) => {
      this.validateFormsDep();
      if (this.validationFailsDep.length == 0) {
        if (this.stockTransferDepartment.packQuantityMove != 0) {
          this.onVoidOkDep($event);
        } else {
          this.stockTransferDepartment.packQuantityMove = null;
          notify("The Pack Quantity can't be 0.", 'error', 5000);
        }
      }
    }
  }

  @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;
  @ViewChild("transferStockLocationForm", { static: false }) transferStockLocationForm: DxFormComponent;
  @ViewChild('content') content;
  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private stockService: StockService, private globalMethodsService: GlobalMethodsService, private authService: AuthenticationService) {
    this.currencyZAR = currencyZAR;
    this.setDepartments = this.setDepartments.bind(this);
    this.getTotalStockDep = this.getTotalStockDep.bind(this);
    this.baseUrl = baseUrl;
    this.setStoreValue = this.setStoreValue.bind(this);
    this.setNewStoreValue = this.setNewStoreValue.bind(this);
    this.setLocations = this.setLocations.bind(this);
    this.getStoreTotal = this.getStoreTotal.bind(this);
    this.priorities = [
      'SKU',
      'UoM'
    ];

    this.dataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/DepartmentStock',
        updateUrl: baseUrl + 'api/DepartmentStock',
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

    this.StockForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Stock/getStockWithQty',
        /* onBeforeSend: (method, param) => {
           param.headers = {
             'Authorization': 'Bearer ' + this.token
           };
         }*/
      }),
      sort: [
        { selector: "InternalProductName", desc: false }
      ],
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

    this.allowModify = this.authService.HavePermission(83);
  }

  onVoidCancel() {
    this.popupTransferStockVisible = false;
    this.stockInfo = null;
    this.stockTransferLocation = new stockTransferLocation();
  }

  onVoidOk(e) {
    this.stockService.TransferStockLocation(this.stockTransferLocation).subscribe(data => {
      if (data != "") {
        this.stockTransferLocation = new stockTransferLocation();
        this.popupTransferStockVisible = false;

        this.dataGrid.instance.getDataSource().reload();
        this.stockInfo = null;
        notify("Stock Transfered.", 'success', 5000);
      } else {
        this.stockTransferLocation = new stockTransferLocation();
        notify("Stock Not Transfered.", 'error', 5000);
      }
    }, subError => {
      notify(subError.error, 'error', 5000);
    });
  }

  validateForms() {
    this.validationFails = [];
    const transferStockLocationValidation = this.addTransferStockFormInstance.validate();
    this.validationFails = this.validationFails.concat(transferStockLocationValidation.brokenRules.map(function (rule) {
      return rule.message
    }));
  }

  savetransferStockLocationFormInstance(e) {
    this.addTransferStockFormInstance = e.component;
  }

  onVoidCancelDep() {
    this.popupTransferStockDepVisible = false;
    this.stockInfo = null;
    this.totalDep = null;
    this.stockTransferDepartment = new stockTransferDepartment();
  }

  onVoidOkDep(e) {
    this.stockService.TransferStockDepartment(this.stockTransferDepartment).subscribe(data => {
      if (data != "") {
        this.stockTransferDepartment = new stockTransferDepartment();
        this.popupTransferStockDepVisible = false;

        this.dataGrid.instance.getDataSource().reload();
        this.stockInfo = null;
        this.totalDep = null;
        notify("Stock Transfered.", 'success', 5000);
      } else {
        this.stockTransferDepartment = new stockTransferDepartment();
        notify("Stock Not Transfered.", 'error', 5000);
      }
    }, subError => {
      notify(subError.error, 'error', 5000);
    });
  }

  validateFormsDep() {
    this.validationFailsDep = [];
    const transferStockDepValidation = this.addTransferStockDepFormInstance.validate();
    this.validationFailsDep = this.validationFailsDep.concat(transferStockDepValidation.brokenRules.map(function (rule) {
      return rule.message
    }));
  }

  savetransferStockDepFormInstance(e) {
    this.addTransferStockDepFormInstance = e.component;
  }

  randFormatConversion(cellInfo) {
    if (cellInfo.value)
      return GlobalMethodsService.formatCurrency(cellInfo.value, "R");
    else
      return cellInfo.value;
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

  setStoreValue(e) {
    if (e.value) {
      this.LocationId = e.value;

      this.StoreForeignDataSource = {
        store: AspNetData.createStore({
          key: 'id',
          loadUrl: this.baseUrl + 'api/Stores/FilterStore?id=' + this.StockId + '&locationId=' + this.LocationId,
        }),
        paginate: true
      }

      this.stockService.GetDefaultStore(this.LocationId).subscribe(data => {
        if (data != null) {
          this.stockTransferLocation.originStore = data[0].defaultStoreId;
        } else {
          this.stockTransferLocation.originStore = null;
        }
      }, subError => {
        notify(subError.error, 'error', 5000);
      });

      this.stockService.getTotalStockLocation(this.LocationId,this.StockId).subscribe(data => {
        this.totalLocation = data;
      }, subError => {
        notify(subError.error, 'error', 5000);
      });
    }
  }

  getStoreTotal(e) {
    if (e.value) {
      this.stockService.getTotalStockStore(this.LocationId,e.value,this.StockId).subscribe(data => {
        this.totalStore = data;
      }, subError => {
        notify(subError.error, 'error', 5000);
      });
    }   
  }

  setNewStoreValue(e) {
    if (e.value) {
      this.NewLocationId = e.value;

      this.NewStoreTypeForeignDataSource = {
        store: AspNetData.createStore({
          key: 'id',
          loadUrl: this.baseUrl + 'api/Stores',
        }),
        filter: ["plantLocationId", "=", this.NewLocationId],
        paginate: true
      }

      this.stockService.GetDefaultStore(this.NewLocationId).subscribe(data => {
        if (data != null) {
          this.stockTransferLocation.newStore = data[0].defaultStoreId;
        } else {
          this.stockTransferLocation.newStore = null;
        }
      }, subError => {
        notify(subError.error, 'error', 5000);
      });
    }
  }

  setLocations(e) {
    if (e.value) {
      this.StockId = e.value;
      this.FilteredLocationForeignDataSource = {
        store: AspNetData.createStore({
          key: 'id',
          loadUrl: this.baseUrl + 'api/PlantLocation/filterLocations?id=' + this.StockId,
        }),
        paginate: true
      }

      this.UomRemoveForeignDataSource = {
        store: AspNetData.createStore({
          key: 'id',
          loadUrl: this.baseUrl + 'api/UnitOfMeasurement/getUOMbyStock?stockId=' + this.StockId,
        }),
        paginate: true
      }
      this.stockService.GetDefaultLocation(this.StockId).subscribe(data => {
        if (data != null) {
          this.stockTransferLocation.originLocation = data[0].id;
        } else {
          this.stockTransferLocation.originLocation = null;
        }
      }, subError => {
        notify(subError.error, 'error', 5000);
      });

      this.stockService.GetUom(this.StockId).subscribe(data => {
        if (data != null) {
          this.stockTransferLocation.uomid = data[0].id;
        } else {
          this.stockTransferLocation.uomid = null;
        }
      }, subError => {
        notify(subError.error, 'error', 5000);
      });


      this.stockService.GetStockData(this.StockId, 1).subscribe(data => {
        this.stockInfo = data;
        this.code = this.stockInfo.code;
        this.productName = this.stockInfo.internalProductName;
        this.uomName = this.stockInfo.uomName;
        this.quantity = this.stockInfo.quantity;
        this.unitQty = this.stockInfo.itemQuantity;
        this.packSize = this.stockInfo.packSize;
        this.packQty = this.stockInfo.packQuantity;        
      }, subError => {
        notify(subError.error, 'error', 5000);
      });
    }
  }
  selectOnChange(e: any) {
    this.StockId = e.value;
  }
  initialUomQuantity(rowData) {
    return rowData.quantity + " (" + rowData.uomName + ")";
  }

  priceISO(rowData) {
    return rowData.supplierCurrencyIso + " " + GlobalMethodsService.formatCurrency(rowData.price, "") ;
  }

  setDepartments(e) {
    if (e.value) {
      this.StockId = e.value;
      this.FilteredDepartmentForeignDataSource = {
        store: AspNetData.createStore({
          key: 'id',
          loadUrl: this.baseUrl + 'api/Department/FilterDepartments?id=' + this.StockId,
        }),
        paginate: true
      }

      this.UomRemoveForeignDataSource = {
        store: AspNetData.createStore({
          key: 'id',
          loadUrl: this.baseUrl + 'api/UnitOfMeasurement/getUOMbyStock?stockId=' + this.StockId,
        }),
        paginate: true
      }

      this.stockService.GetDefaultLocation(this.StockId).subscribe(data => {
        if (data != null) {
          this.stockTransferDepartment.originDepartment = data[0].id;
        } else {
          this.stockTransferDepartment.originDepartment = null;
        }
      }, subError => {
        notify(subError.error, 'error', 5000);
      });

      this.stockService.GetUom(this.StockId).subscribe(data => {
        if (data != null) {
          this.stockTransferDepartment.uomid = data[0].id;
        } else {
          this.stockTransferDepartment.uomid = null;
        }
      }, subError => {
        notify(subError.error, 'error', 5000);
      });

      this.stockService.GetStockData(this.StockId, 1).subscribe(data => {
        this.stockInfo = data;
        this.code = this.stockInfo.code;
        this.productName = this.stockInfo.internalProductName;
        this.uomName = this.stockInfo.uomName;
        this.quantity = this.stockInfo.quantity;
        this.unitQty = this.stockInfo.itemQuantity;
        this.packSize = this.stockInfo.packSize;
        this.packQty = this.stockInfo.packQuantity;
      }, subError => {
        notify(subError.error, 'error', 5000);
      });
    }
  }

  getTotalStockDep(e) {
    if (e.value) {
      this.stockService.getTotalStockDep(e.value, this.StockId).subscribe(data => {
        this.totalDep = data;
      }, subError => {
        notify(subError.error, 'error', 5000);
      });
    }
  }
}
