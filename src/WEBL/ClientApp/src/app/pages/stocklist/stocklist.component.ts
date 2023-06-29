import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DxDataGridComponent, DxFormComponent } from 'devextreme-angular';
import { DxiItemComponent } from 'devextreme-angular/ui/nested';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import { exportDataGrid } from 'devextreme/excel_exporter';
import notify from 'devextreme/ui/notify';
import { Workbook } from 'exceljs';
import saveAs from 'file-saver';
import { currencyZAR, EnumRecipe } from 'src/globalConstants';
import { stockQuantity } from '../../core/models/stockQuantity.models';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { StockService } from '../../core/services/stock.service';
import { tab } from "../models/tab";
import { OrdersService } from '../../core/services/orders.service';

@Component({
  selector: 'app-stocklist',
  templateUrl: './stocklist.component.html',
  styleUrls: ['./stocklist.component.scss']
})
export class StocklistComponent implements OnInit {
  dataSource: any;
  DepdataSource: any;
  UnitofMeasureForeignDataSource: any;
  StockCategoryForeignDataSource: any;
  StockForeignDataSource: any;
  DepartmentForeignDataSource: any;
  UomRemoveForeignDataSource: any;
  LocationForeignDataSource: any;
  StoreForeignDataSource: any;
  StockGroupForeignDataSource: any;
  SupplierForeignDataSource: any;
  CurrencyForeignDataSource: any;
  StoreTypeForeignDataSource: any;
  StorageTypeForeignDataSource: any;
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  currentDataSource: any;
  showCustomMixTab = false;
  popupManageStockVisible = false;
  tabs: tab[];
  stockQuantity: stockQuantity = new stockQuantity();
  itemMixingDataSource: any;
  error = false;
  option: any;
  MainStockId: any;
  baseUrl: any;
  priceNotSet = false;
  isRecipe = false;
  stockCategory: any;
  rowIndex = 0;
  pageName: string = "Stock";
  uom: any;
  code: any;
  productName: any;
  stockId: any;
  validationFails = [];
  resetStockQuantityFormInstance: any;
  removeStockQuantityFormInstance: any;
  addStockQuantityFormInstance: any;
  CodeAndName: any;
  currencyZAR: any;
  event: any;
  CancelDownload = true;
  popupDownloadConfirmVisible = false;
  oldCode = "";
  allowModify = false;
  allowManage = false;
  defaultVisible = false;
  LocationId = null;
  editingMode = false;
  show = false;
  managestockinfo = "";
  popupUomQ = 0;
  popupUom2Q = 0;
  popupPackSize = 0;
  popupEstimatedPacks = 0;
  layouts: string[];
  MasterlistGrid = true;
  DepartmentGrid = false;
  LocationGrid = false; 
  layoutValue = 0;
  StockPage = "Masterlist"
  //#region Editor Options

  noDownloadButtonOptions: any = {
    text: "No",
    onClick: (e) => {
      this.popupDownloadConfirmVisible = false;
      this.CancelDownload = true;

    }
  }

  yesDownloadButtonOptions = {
    text: "Yes",
    onClick: () => {
      this.popupDownloadConfirmVisible = false;

      this.event.component.beginUpdate();
      this.event.component.columnOption('minThreshold', 'visible', true);
      
      const workbook = new Workbook();
      const worksheet = workbook.addWorksheet('Main sheet');
      exportDataGrid({
        component: this.event.component,
        worksheet: worksheet
      }).then(function () {
        workbook.xlsx.writeBuffer()
          .then(function (buffer: BlobPart) {
            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Stock-List.xlsx');
          });
      })
      .then(function () {
        this.event.component.columnOption('minThreshold', 'visible', false);
        this.event.component.endUpdate();
      });
      this.event.cancel = true;
    }
  }

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
        case "add": {
          this.validateForms(this.option);
          if (this.validationFails.length == 0) {
            this.onVoidOk($event);
            this.popupManageStockVisible = false;
          }
          break;
        }
        case "remove": {
          this.validateForms(this.option);
          if (this.validationFails.length == 0) {
            this.onVoidOk($event);
            this.popupManageStockVisible = false;
          }
          break;
        }
        case "set": {
          this.validateForms(this.option);
          if (this.validationFails.length == 0) {
            this.onVoidOk($event);
            this.popupManageStockVisible = false;
          }
          break;
        }
        case "recipe": {
          if (this.itemMixingDataSource.recipe.length < 2) {
            this.validationFails.push("Minimum of two items are required.");
          }
          if (this.itemMixingDataSource.recipe.length >= 2) {
            this.onVoidOk($event);
            this.validationFails = [];
            this.popupManageStockVisible = false;
          }
          break;
        }
      }
    }
  }

  //#endregion

  @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;
  @ViewChild("resetStockQuantityForm", { static: false }) resetStockQuantityForm: DxFormComponent;
  @ViewChild("removeStockQuantityForm", { static: false }) removeStockQuantityForm: DxFormComponent;
  @ViewChild("addStockQuantityForm", { static: false }) addStockQuantityForm: DxFormComponent;
  @ViewChild("code", { static: false }) codeInputField: DxiItemComponent;
  @ViewChild('content') content;

  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private stockService: StockService, private globalMethodsService: GlobalMethodsService, private router: Router, private orderService: OrdersService, private route: ActivatedRoute, private authService: AuthenticationService) {
    this.currencyZAR = currencyZAR;
    this.baseUrl = baseUrl;
    this.isRecipe = false;
    this.rowIndex = 0;
    this.option = "add";
    this.buildtabs(this.showCustomMixTab);
    this.layouts = ['Masterlist', 'Department Stock','Location Stock'];

    this.route.queryParamMap.subscribe(queryParams => {
      this.code = queryParams.get('code');
      this.productName = queryParams.get('productName');
    });

    this.dataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Stock',
        deleteUrl: baseUrl + 'api/Stock',
        onInserted: () => {
          this.globalMethodsService.ToastInsertSuccessMessage(this.pageName);
        },
        onUpdated: () => {
          this.globalMethodsService.ToastUpdateSuccessMessage(this.pageName);
        },
        onRemoved: () => {
          this.globalMethodsService.ToastDeleteSuccessMessage(this.pageName);
        },
        onAjaxError: () => {
          window.scroll({
            top: 0,
            left: 0,
            behavior: 'smooth'
          });
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

    this.DepdataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/DepartmentStock'
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

    this.StockForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Stock',
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

    this.SupplierForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Supplier',
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

    this.StorageTypeForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/StorageType',
      }),
      paginate: true
    }

    this.stockCategory = {
      onValueChanged: (e: { value: number; }) => {
        var oldCodeNot = this.dataGrid.instance.cellValue(this.rowIndex, "code");
        
        if (oldCodeNot && oldCodeNot != "REC") {
          this.oldCode = oldCodeNot;
        }

        if (e.value == EnumRecipe) {
          this.isRecipe = true;
          this.dataGrid.instance.cellValue(this.rowIndex, "stockCategoryId", e.value);
          this.dataGrid.instance.cellValue(this.rowIndex, "code", "REC");
        } else {
          this.isRecipe = false;
          this.dataGrid.instance.cellValue(this.rowIndex, "stockCategoryId", e.value);
          this.dataGrid.instance.cellValue(this.rowIndex, "code", this.oldCode);          
        }        
      }
    }

    this.allowModify = this.authService.HavePermission(13);
    this.allowManage = this.authService.HavePermission(116);
  }

  /*saveResetStockQuantityFormInstance(e) {
    this.resetStockQuantityFormInstance = e.component;
  }*/

  saveRemoveStockQuantityFormInstance(e) {
    this.removeStockQuantityFormInstance = e.component;
  }

  saveAddStockQuantityFormInstance(e) {
    this.addStockQuantityFormInstance = e.component;
  }

  validateForms(option) {
    this.validationFails = [];

    switch (option) {
      case "add": {
        const addStockQuantityValidation = this.addStockQuantityFormInstance.validate();
        this.validationFails = this.validationFails.concat(addStockQuantityValidation.brokenRules.map(function (rule) {
          return rule.message
        }));
        break;
      }
      case "remove": {
        const removeStockQuantityValidation = this.removeStockQuantityFormInstance.validate();
        this.validationFails = this.validationFails.concat(removeStockQuantityValidation.brokenRules.map(function (rule) {
          return rule.message
        }));
        break;
      }
      /*case "set": {
        const resetStockQuantityValidation = this.resetStockQuantityFormInstance.validate();
        this.validationFails = this.validationFails.concat(resetStockQuantityValidation.brokenRules.map(function (rule) {
          return rule.message
        }));
        break;
      }*/
    }    
  }

  randFormatConversion(cellInfo) {
    if (cellInfo.value)
      return GlobalMethodsService.formatCurrency(cellInfo.value, "R");
    else
      return cellInfo.value;
  }

  buildtabs(visible) {
    /*this.tabs = [
      { text: "Add Stock" },
      { text: "Remove Stock" },
      { text: "Stock Recipe" }
     *//* { text: "Reset Stock" }*//*
    ];*/
    if (visible) {
      this.tabs = [
        { text: "Add Stock" },
        { text: "Remove Stock" },
        { text: "Stock Recipe" }
      ];
    } else {
      this.tabs = [
        { text: "Add Stock" },
        { text: "Remove Stock" }
      ];
    }
  }

  getStock(rowData) {
    return rowData.code + " - " + rowData.productName;
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

  onContentReady(e) {
    this.filter();
  }

  onOptionChanged(e) {    
    if (e) {
      this.stockQuantity.itemQuantity = null;
      switch(e.addedItems[0].text){
        case "Add Stock": {
          this.option = "add";
          this.managestockinfo = "Enter the total quantity packs/rolls below";
          break;
        }
        case "Remove Stock": {
          this.option = "remove";
          this.managestockinfo = "Enter the total UoM you want to remove below";
          break;
        }
       /* case "Reset Stock": {
          this.option = "set";
          break;
        }*/
        case "Stock Recipe": {
          this.option = "recipe";
          break;
        }
        default: {
          this.option = "";
          break;
        }
      }
    }
  }

  manageStockPrepared(e) {
    if (e.rowType === "data" && e.column.caption == "Manage Stock") {
      e.cellElement.style.color = "#1f7556";
      e.cellElement.style.backgroundColor = "#d6f3e9";
    }
  }

  manageStockOnClick(e) {
    if (e.rowType === "data" && e.column.caption == "Manage Stock") {      
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
          loadUrl: this.baseUrl + 'api/UnitOfMeasurement/getUOMbyStock?stockId=' + this.stockId,          
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

     /* this.stockQuantity = new stockQuantity();
      this.stockService.GetStockPrice(e.data.id).subscribe(data => {
        if (data != null) {
          if (e.rowType === "data" && e.column.caption == "Manage Stock") {
            this.popupManageStockVisible = true;

            if (data[0] === undefined) {
              //if no price is set
              this.priceNotSet = true;
            } else {
              //if price is set
              this.stockQuantity.price = data[0].price;
              this.stockQuantity.stockId = e.data.id;
              this.priceNotSet = false;
            }
          }
        } else {
          this.stockQuantity = new stockQuantity();
          notify("An error occured.", 'error', 5000);
        }
      }, subError => {
        notify(subError.error, 'error', 5000);
      });*/
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
      case "add": {
        this.stockQuantity.stockId = this.stockId;
        this.stockService.AddStock(this.stockQuantity).subscribe(data => {
          if (data != "") {
            this.stockQuantity = new stockQuantity();
            this.popupManageStockVisible = false;

            this.dataGrid.instance.getDataSource().reload();

            notify("Quantity Added.", 'success', 5000);
          } else {
            this.stockQuantity = new stockQuantity();
            notify("Quantity Not Added.", 'error', 5000);
          }
        }, subError => {         
            notify(subError.error, 'error', 5000);
        });
        break;
      }
      case "remove": {
        this.stockQuantity.stockId = this.stockId;
        this.stockService.RemoveStock(this.stockQuantity).subscribe(data => {
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
      /*case "set": {
        this.stockService.SetStock(this.stockQuantity).subscribe(data => {
          if (data != "") {
            this.stockQuantity = new stockQuantity();
            this.popupManageStockVisible = false;

            this.dataGrid.instance.getDataSource().reload();

            notify("Quantity Set.", 'success', 5000);
          } else {
            notify("Quantity Not Set.", 'error', 5000);
          }
        }, subError => {
            notify(subError.error, 'error', 5000);
        });
        break;
      }*/
      case "recipe": {
        this.stockService.MixStock(this.itemMixingDataSource).subscribe(data => {
          if (data != "") {
            notify("New Stock Item Mixed.", 'success', 5000);
          } else {
            notify("New Stock items not mixed.", 'error', 5000);
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

  EditStart(e) {
    this.rowIndex = this.dataGrid.instance.getRowIndexByKey(e.data.id);
    this.router.navigate([`/stocks-modify`], { state: { id: e.data.id, action: 2 } }); 
    this.oldCode = "";
    this.editingMode = false;
    //this.rowIndex = this.dataGrid.instance.getRowIndexByKey(e.data.id);
    if (e.data.stockCategoryId == EnumRecipe) {      
      this.isRecipe = true;
    } else {
      this.isRecipe = false;
    }
  }

  newRow(e) {
    this.router.navigate([`/stocks-modify`],{ state: { id: null, action: 1} }); 
    this.oldCode = "";
    this.isRecipe = false;
    this.editingMode = true;
    e.data.monitored = false;
  }

  DeleteItem(e) {
    this.itemMixingDataSource.recipe.splice(e, 1);
  }

  AddItem() {
    this.validationFails = [];
    this.itemMixingDataSource.recipe.push({ id: 0, stockId: this.MainStockId, stockComponentId: null, quantity: 0 });
  }

  SavingStart(e) {
    this.rowIndex = 0;
    this.isRecipe = false;
  }

  filter() {
    if (this.productName != null) {
      this.dataGrid.instance.filter([["productName", "=", this.productName], "and", ["code", "=", this.code]]);
    }
  }

  navigatePriceLookUp() {
    this.navigateToPriceLookUp(this.stockId);
  }

  navigateToPriceLookUp(stockId) {
    this.router.navigate([`/price-lookup`], { queryParams: { stockId: stockId }, relativeTo: this.route }); 
  }

  ngAfterViewInit() {

  }

  onHidden(e) {
    this.dataGrid.instance.getDataSource().reload();
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

  onValueChanged(e) {
    switch (e.value) {
      case "Masterlist": {
        this.MasterlistGrid = true;
        this.DepartmentGrid = false;
        this.LocationGrid = false;
        this.StockPage = "Masterlist";
        break;
      }
      case "Department Stock": {
        this.MasterlistGrid = false;
        this.DepartmentGrid = true;
        this.LocationGrid = false;
        this.StockPage = "Department Stock";
        break;
      }
      case "Location Stock": {
        this.MasterlistGrid = false;
        this.DepartmentGrid = false;
        this.LocationGrid = true;
        this.StockPage = "Location Stock";
        break;
      }
    }
  }

  toggleDefault() {
    this.defaultVisible = !this.defaultVisible;
  }

  concatPack(rowData) {
    return rowData.internalProductName + " (" + rowData.packSize + " Pack)";
  }

  initialUomQuantity(rowData) {
    if (rowData.productionQuantity > 0) {
      return (rowData.quantity - rowData.productionQuantity) + " (" + rowData.uomName + ")";
    } else {
      return (rowData.quantity) + " (" + rowData.uomName + ")";
    }
    
/* return (rowData.quantity - rowData.quarantineQuantity) + " (" + rowData.uomName + ")";*/
  }

  quarantineUomQuantity(rowData) {
    return rowData.quarantineQuantity + " (" + rowData.uomName + ")";
  }

  priceISO(rowData) {
    return rowData.supplierCurrencyIso + " " + GlobalMethodsService.formatCurrency(rowData.price,"") ;
  }

  secondUomQuantity(rowData) {
    if (rowData.secondQuantity != 0 && rowData.secondUomName != null) {
      return rowData.secondQuantity + " (" + rowData.secondUomName + ")";
    } else {
      return rowData.secondQuantity;
    }
   
  }
}
