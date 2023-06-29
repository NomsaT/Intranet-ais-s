import { AfterViewInit, Component, Inject, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DxDataGridComponent, DxFormComponent } from 'devextreme-angular';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import { exportDataGrid } from 'devextreme/excel_exporter';
import notify from 'devextreme/ui/notify';
import { Workbook } from 'exceljs';
import saveAs from 'file-saver';
import { currencyZAR } from 'src/globalConstants';
import { stockTransferDepartment } from '../../core/models/stockTransferDepartment.models';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { StockService } from '../../core/services/stock.service';
import { QuarantineService } from '../../core/services/quarantine.service';

@Component({
  selector: 'app-department-stock',
  templateUrl: './department-stock.component.html',
  styleUrls: ['./department-stock.component.scss']
})
export class DepartmentStockComponent implements AfterViewInit {
  dataSource: any;
  UnitofMeasureForeignDataSource: any;
  StockCategoryForeignDataSource: any;
  DepartmentForeignDataSource: any;
  FilteredDepartmentForeignDataSource: any;
  StockForeignDataSource: any;
  LocationForeignDataSource: any;
  StoreForeignDataSource: any;
  StoreTypeForeignDataSource: any;
  StockGroupForeignDataSource: any;
  CurrencyForeignDataSource: any;
  StorageTypeForeignDataSource: any;
  SupplierForeignDataSource: any;
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  currencyZAR: any;
  event: any;
  CancelDownload = true;
  popupDownloadConfirmVisible = false;
  allowReturn = false;
  validationFails = [];
  popupTransferStockDepVisible = false;
  stockTransferDepartment: stockTransferDepartment = new stockTransferDepartment();
  addTransferStockFormInstance: any;
  StockId = null;
  baseUrl: string;
  stockInfo: any;
  code: any;
  productName: any;
  uomName: any;
  quantity: any;
  unitQty: any;
  packSize: any;
  packQty: any;
  priorities: string[];
  UomRemoveForeignDataSource: any;
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
            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Department-List.xlsx');
          });
      });
    }
  };

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
      if (this.validationFails.length == 0) {
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
  @ViewChild("transferStockDepForm", { static: false }) transferStockDepForm: DxFormComponent;
  @ViewChild('content') content;
  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private stockService: StockService, private globalMethodsService: GlobalMethodsService, private authService: AuthenticationService, private quarantineService: QuarantineService) {
    this.currencyZAR = currencyZAR;
    this.setDepartments = this.setDepartments.bind(this);
    this.getTotalStockDep = this.getTotalStockDep.bind(this);
    this.baseUrl = baseUrl;
    this.priorities = [
      'SKU',
      'UoM'
    ];

    this.dataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/QuarantineStock'
        /*onBeforeSend: (method, param) => {
          param.headers = {
            'Authorization': 'Bearer ' + this.authService.getToken()
          };
        }*/
      }),
      paginate: true,
      sort: ([
        { selector: "stockId", desc: false }
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
      sort: [
        { selector: "Name", desc: false }
      ],
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

    this.SupplierForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Supplier',
      }),
      paginate: true
    }

    this.allowReturn = this.authService.HavePermission(119);
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
    this.validationFails = [];
    const transferStockDepValidation = this.addTransferStockFormInstance.validate();
    this.validationFails = this.validationFails.concat(transferStockDepValidation.brokenRules.map(function (rule) {
      return rule.message
    }));
  }

  savetransferStockDepFormInstance(e) {
    this.addTransferStockFormInstance = e.component;
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
      this.stockService.getTotalStockDep(e.value,this.StockId).subscribe(data => {
        this.totalDep = data;
      }, subError => {
        notify(subError.error, 'error', 5000);
      });  
    }
  }

  initialUomQuantity(rowData) {
    return rowData.quantity + " (" + rowData.uomName + ")";
  }

  priceISO(rowData) {
    return rowData.supplierCurrencyIso + " " + GlobalMethodsService.formatCurrency(rowData.price, "") ;
  }

  returnStockPrepared(e) {
    if (e.rowType === "data" && e.column.caption == "Return to Supplier") {
      e.cellElement.style.color = "#1f7556";
      e.cellElement.style.backgroundColor = "#d6f3e9";
    }
  }

  returnStockOnClick(e) {
    if (e.rowType === "data" && e.column.caption == "Return to Supplier") {
      alert("test");
    }
  }

  onToolbarPreparing(e): void {

    if (this.allowReturn == true) {
      e.toolbarOptions.items.unshift({
        location: 'after',
        widget: 'dxButton',
        options: {
          icon: 'print',
          text: 'Return Selection',
          type: "normal",
          useSubmitBehavior: false,
          onClick: () => {
            this.quarantineService.ReturnToSupplier(this.dataGrid.instance.getSelectedRowsData()).subscribe(data => {
              if (data != null && data != undefined) {
                this.dataGrid.instance.clearSelection();
                this.dataGrid.instance.getDataSource().reload();
              }
            }, subError => {
              notify(subError.error, 'error', 5000);
            });
          }
        }
      });
      e.toolbarOptions.items.unshift({
        location: 'after',
        widget: 'dxButton',
        options: {
          icon: 'clear',
          text: 'Clear Selection',
          type: "normal",
          useSubmitBehavior: false,
          onClick: () => {
            this.dataGrid.instance.clearSelection();
          }
        }
      });
    }
  }
}
