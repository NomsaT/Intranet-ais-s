import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import notify from 'devextreme/ui/notify';
import { currencyZAR, EnumRecipe } from 'src/globalConstants';
import { stockItem } from '../../core/models/stockItem.models';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { OrdersService } from '../../core/services/orders.service';
import { popupAutoSelectService } from '../../core/services/popupAutoSelect.service';
import { StockService } from '../../core/services/stock.service';

@Component({
  selector: 'app-stocks-modify',
  templateUrl: './stocks-modify.component.html',
  styleUrls: ['./stocks-modify.component.scss']
})
export class StockModifyComponent implements OnInit {
  UnitofMeasureForeignDataSource: any;
  StockCategoryForeignDataSource: any;
  StockForeignDataSource: any;
  DepartmentForeignDataSource: any;
  DepartmentRemoveForeignDataSource: any;
  LocationForeignDataSource: any;
  StoreForeignDataSource: any;
  BinForeignDataSource: any;
  StockGroupForeignDataSource: any;
  ShelfLifeTypeForeignDataSource: any;
  SupplierForeignDataSource: any;
  StorageTypeForeignDataSource: any;
  popupVisible = false;
  popupVisibleS = false;
  popupVisibleSC = false;
  popupVisibleU = false;
  popupVisibleDepartment = false;
  popupVisibleStorage = false;
  popupVisibleLocationStore = false;
  popupVisibleBin = false;
  popupManageStockVisible = false;
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  pageName: string = "Stock Item";
  event: any;
  actionDisplay: any;
  action: any;
  id: any;
  data: any;
  currencyZAR: any;
  allowModify = false;
  allowAddingQuantity = false;
  LocationId = null;
  StoreId = null;
  uomID = null;
  baseUrl: string;
  editingMode = false;
  newState = true;
  selecteduom: any;
  showError = false;
  refreshLocationOnly = false;
  currency = "ZAR";
  loadingVisible = false;
  oldCode = "";
  isRecipe = false;

  AddUpdateStockBtn: any = {
    text: "Submit",
    type: "success",
    useSubmitBehavior: true,
    validationGroup: "stockData",
    onClick: (e) => {
      console.log("submit btn pressed");
      const res = e.validationGroup.validate();
      console.log(res);
      if (res.isValid) {
        if ((this.data.secondaryUomid == null && this.data.comparisonSecondValue != 0) || (this.data.secondaryUomid != null && this.data.comparisonSecondValue == 0)) {
          this.showError = true;
        } else {
          this.showError = false;
          this.AddUpdateStock();
        }
      } else {
        window.scroll({
          top: 0,
          left: 0,
          behavior: 'smooth'
        });
      }
    }
  }

  AddUpdateStockCancelBtn: any = {
    text: "Cancel",
    type: "default",
    useSubmitBehavior: true,
    onClick: () => {
      this.data = "";
      this.router.navigate([`/stocklist`], { relativeTo: this.route });
    }
  }

  buttonOptions: any = {
    icon: "plus",
    type: "default",
    hint: "Add a new stock group",
    useSubmitBehavior: false,
    onClick: () => {
      this.popupVisible = true;
    }
  }

  buttonOptionsS: any = {
    icon: "plus",
    type: "default",
    hint: "Add a new supplier",
    useSubmitBehavior: false,
    onClick: () => {
      this.popupVisibleS = true;
    }
  }

  buttonOptionsSC: any = {
    icon: "plus",
    type: "default",
    hint: "Add a new stock category",
    useSubmitBehavior: false,
    onClick: () => {
      this.popupVisibleSC = true;
    }
  }

  buttonOptionsU: any = {
    icon: "plus",
    type: "default",
    hint: "Add a new UoM",
    useSubmitBehavior: false,
    onClick: () => {
      this.popupVisibleU = true;
    }
  }

  buttonOptionsDepartment: any = {
    icon: "plus",
    type: "default",
    hint: "Add a new department",
    useSubmitBehavior: false,
    onClick: () => {
      this.popupVisibleDepartment = true;
    }
  }

  buttonOptionsStorageType: any = {
    icon: "plus",
    type: "default",
    hint: "Add a new storage type",
    useSubmitBehavior: false,
    onClick: () => {
      this.popupVisibleStorage = true;
    }
  }

  buttonOptionsLocation: any = {
    icon: "plus",
    type: "default",
    hint: "Add a new location",
    useSubmitBehavior: false,
    onClick: () => {
      this.popupVisibleLocationStore = true;
      this.refreshLocationOnly = true;
    }
  }

  buttonOptionsStore: any = {
    icon: "plus",
    type: "default",
    hint: "Add a new store",
    useSubmitBehavior: false,
    onClick: () => {
      this.popupVisibleLocationStore = true;
      this.refreshLocationOnly = false;
    }
  }

  buttonOptionsBin: any = {
    icon: "plus",
    type: "default",
    hint: "Add a new bin",
    useSubmitBehavior: false,
    onClick: () => {
      this.popupVisibleBin = true;
      this.refreshLocationOnly = false;
    }
  }

  buttonClear: any = {
    icon: "trash",
    type: "default",
    hint: "Clear",
    useSubmitBehavior: false,
    onClick: () => {
      this.data.secondaryUomid = null;
      this.data.totalweight = 0;
      this.data.comparisonSecondValue = 0;
      this.data.calculatedRatio = 0;
      this.data.deductedValue = 0;
    }
  }

  @ViewChild('content') content;
  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private globalMethodsService: GlobalMethodsService, private stockService: StockService, private autoSelect: popupAutoSelectService, private orderService: OrdersService, private router: Router, private route: ActivatedRoute, private authService: AuthenticationService) {
    this.currencyZAR = currencyZAR;
    this.isRecipe = false;
    this.action = router.getCurrentNavigation().extras.state.action;
    this.id = router.getCurrentNavigation().extras.state.id;
    this.refreshLocationOnly = false;
    this.baseUrl = baseUrl;
    this.GetStockData();

    this.setStoreValue = this.setStoreValue.bind(this);
    this.setBinValue = this.setBinValue.bind(this);
    this.setChangeQuantity = this.setChangeQuantity.bind(this);
    this.setChangePack = this.setChangePack.bind(this);
    this.setUomValue = this.setUomValue.bind(this);
    this.setCalculatedRatio = this.setCalculatedRatio.bind(this);
    this.ChangedTotal = this.ChangedTotal.bind(this);
    this.ChangedDeductedValue = this.ChangedDeductedValue.bind(this);
    this.changeCurrentPrice = this.changeCurrentPrice.bind(this);
    this.checkSKU = this.checkSKU.bind(this);
    this.setMinThreshold = this.setMinThreshold.bind(this);
    this.setCurrency = this.setCurrency.bind(this);
    this.stockCategory = this.stockCategory.bind(this);

    this.UnitofMeasureForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/UnitOfMeasurement',
      }),
      sort: [
        { selector: "Name", desc: false }
      ],
      paginate: true
    }

    this.StockCategoryForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/StockCategory',
      }),
      sort: [
        { selector: "Name", desc: false }
      ],
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
      sort: [
        { selector: "Name", desc: false }
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
      sort: [
        { selector: "Name", desc: false }
      ],
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
      sort: [
        { selector: "Name", desc: false }
      ],
      paginate: true
    }

    this.BinForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Bins',
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
      sort: [
        { selector: "Name", desc: false }
      ],
      paginate: true
    }

    this.ShelfLifeTypeForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/ShelfLifeType',
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
      sort: [
        { selector: "CompanyName", desc: false }
      ],
      paginate: true
    }

    this.StorageTypeForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/StorageType',
      }),
      sort: [
        { selector: "Name", desc: false }
      ],
      paginate: true
    }

    this.allowModify = this.authService.HavePermission(13);
    this.allowAddingQuantity = this.authService.HavePermission(79);
  }

  GetStockData() {
    switch (this.action) {
      case 1: this.actionDisplay = "Add New Stock Item";
        this.newState = true;
        this.editingMode = true;
        this.oldCode = "";
        this.isRecipe = false;
        this.data = new stockItem();
        this.data.packQuantity = 0;
        this.data.itemPrice = 0;
        this.data.currentPrice = 0;
        this.data.calculatedRatio = 0;
        this.data.deductedValue = 0;
        this.data.comparisonSecondValue = 0;
        this.data.defaultDepartmentId = null;
        this.data.storageTypeId = 19;
        this.orderService.getDefaultDepartment().subscribe(dep => {
          if (dep != null) {
            this.data.defaultDepartmentId = dep;
          }
        });
        break;
      case 2: this.actionDisplay = "Update Stock Item";
        this.newState = false;
        this.editingMode = false;
        this.oldCode = "";
        this.stockService.GetStockData(this.id, this.action).subscribe(data => {
          this.data = data;
          this.data.totalweight = this.data.comparisonSecondValue + this.data.deductedValue;
          if (this.data.stockCategoryId == EnumRecipe) {
            this.isRecipe = true;
          } else {
            this.isRecipe = false;
          }
          this.stockService.GetCurrency(this.data.supplierId).subscribe(data => {
            if (data != null) {
              this.data.currency = data["iso"];
              this.currency = data["iso"];
            }
          });
        }, subError => {
          notify(subError.error, 'error', 5000);
        });
        break;
    }
  }

  AddUpdateStock() {
    console.log(this.authService.currentUser().username);
    let username = this.authService.currentUser().username;
    Object.assign(this.data, { username: username });

    this.loadingVisible = true;
    this.stockService.AddUpdateStock(this.data).subscribe(data => {
      this.router.navigate([`/stocklist`], { relativeTo: this.route });
      this.loadingVisible = false;
      notify('Stock Item Updated', 'success', 2000);
    }, subError => {
      this.loadingVisible = false;
      notify(subError.error, 'error', 4000);
    });
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

  onHiddencallstoragetype(e) {
    this.StorageTypeForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/StorageType',
      }),
      paginate: true
    }
    this.autoSelect.GetNewestStorageType().subscribe(data => {
      this.data.storageTypeId = data;
    }, subError => {
      notify(subError.error, 'error', 5000);
    });
  }

  onHiddencallstockgroup(e) {
    this.StockGroupForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/StockGroup',
      }),
      paginate: true
    }
    this.autoSelect.GetNewestStockGroup().subscribe(data => {
      this.data.stockGroupId = data;
    }, subError => {
      notify(subError.error, 'error', 5000);
    });
  }

  onHiddencallsuppliers(e) {
    this.SupplierForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/Supplier',
      }),
      paginate: true
    }
    this.autoSelect.GetNewestSupplier().subscribe(data => {
      this.data.supplierId = data;
    }, subError => {
      notify(subError.error, 'error', 5000);
    });

  }

  onHiddencallstockcategory(e) {
    this.StockCategoryForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/StockCategory',
      }),
      paginate: true
    }
    this.autoSelect.GetNewestStockCategory().subscribe(data => {
      this.data.stockCategoryId = data;
    }, subError => {
      notify(subError.error, 'error', 5000);
    });
  }

  onHiddencalluom(e) {
    this.UnitofMeasureForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/UnitOfMeasurement',
      }),
      paginate: true
    }
    this.autoSelect.GetNewestUom().subscribe(data => {
      this.data.uomid = data;
    }, subError => {
      notify(subError.error, 'error', 5000);
    });
  }

  onHiddencalldepartment(e) {
    this.orderService.getDefaultDepartment().subscribe(dep => {
      if (dep != null) {
        this.data.defaultDepartmentId = dep;
      }
    }, subError => {
      notify(subError.error, 'error', 2000);
    });
    this.autoSelect.GetNewestDepartment().subscribe(data => {
      this.data.defaultDepartmentId = data;
    }, subError => {
      notify(subError.error, 'error', 5000);
    });
  }

  onHiddencallstoreandlocation(e) {
    this.LocationForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/PlantLocation',
      }),
      paginate: true
    }

    if (this.refreshLocationOnly) {
      this.autoSelect.GetNewestLocation().subscribe(data => {
        this.data.defaultLocationId = data;
      }, subError => {
        notify(subError.error, 'error', 5000);
      });
    }

    this.StoreForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/Stores',
      }),
      paginate: true
    }
  }

  onHiddencallbin(e) {
    this.BinForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/Bins',
      }),
      paginate: true
    }
  }

  ngAfterViewInit() {
  }

  setStoreValue(e) {
    this.LocationId = e.value;
    this.StoreForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/Stores',
      }),
      filter: ["plantLocationId", "=", this.LocationId],
      paginate: true
    }

    this.stockService.GetDefaultStore(this.LocationId).subscribe(data => {
      if (data != null) {
        this.data.defaultStoreId = data[0].defaultStoreId;
      } else {
        this.data.defaultStoreId = null;
      }
    });
  }

  setBinValue(e) {
    this.StoreId = e.value;
    this.BinForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/Bins',
      }),
      filter: ["storeId", "=", this.StoreId],
      paginate: true
    }
    this.data.binId = null;
  }

  //info
  //Pack Price = itemPrice
  //Pack Qty = packQuantity
  //Pack Size = packSize
  //Unit Price = currentPrice
  //Unit Qty (UOM) = itemQuantity

  setChangeQuantity(e) { //Unit Qty (UOM)
    let packsize: number = this.data.packSize;
    let itemquantity: number = e.value;
    let packquantity: number = packsize * itemquantity;
    this.data.packQuantity = packquantity;

    let packprice: number = this.data.currentPrice; //e.itemQuantity; //unit price (price per UOM) //get unit price
    let packQty: number = this.data.packQuantity;
    if (packQty != 0) {
      let calculation: number = packprice * packQty;
      this.data.itemPrice = calculation;
    } else {
      this.data.itemPrice = 0;
    }

    let calculatedVal: number = this.data.comparisonSecondValue;
    let packQ: number = this.data.packQuantity;
    let calculation: number = calculatedVal / packQ;
    this.data.calculatedRatio = Number(calculation.toFixed(10));
  }

  changeCurrentPrice(e) { //Unit Price
    let packprice: number = e.value; //unit price (price per UOM)
    //let packsize: number = this.data.packSize;
    let packQty: number = this.data.packQuantity;
    if (packQty != 0) {
      let calculation: number = packprice * packQty;
      this.data.itemPrice = calculation;
    } else {
      this.data.itemPrice = 0;
    }

  }

  setChangePack(e) { //Pack Size
    //Pack Qty Input
    let itemquantity: number = this.data.itemQuantity; //unit Qty
    let packsize: number = e.value; //packsize
    let packquantity: number = itemquantity * packsize;

    if (itemquantity != 0) {
      this.data.packQuantity = packquantity; //pack qty
    } else {
      this.data.packQuantity = 0; //pack qty
    }

    //Pack Price Input
    let unitprice: number = this.data.currentPrice; //Unit Price
    let calculation: number = unitprice * packquantity;
    this.data.itemPrice = calculation; //Pack Price

    //Calculated Ratio Input
    let calculatedVal: number = this.data.comparisonSecondValue;
    let packQ: number = this.data.packQuantity;
    let calculations: number = calculatedVal / packQ;
    this.data.calculatedRatio = Number(calculations.toFixed(10));
  }

  setUomValue(e) {
    this.uomID = e.value;
    this.stockService.GetStockUOM(this.uomID).subscribe(data => {
      if (data != null) {
        this.selecteduom = "(" + data[0].name + ")";
      }
    });
  }

  setCalculatedRatio(e) {
    let calculatedVal: number = e.value;
    let packQ: number = this.data.packQuantity;
    let calculation: number = calculatedVal / packQ;
    this.data.calculatedRatio = Number(calculation.toFixed(10));
  }

  checkSKU(e) {
    /*let id = 0;
    if (this.data.id) {
      id = this.data.id;
    }
    if (!e.value) {
      e.value = "";
    }
    return new Promise((resolve) => {      
      this.stockService.CheckSKU(e.value, id).subscribe(data => {
        resolve(data);        
      });
    });*/
  }

  ChangedTotal(e) {
    let totalweight: number = e.value;
    let core: number = this.data.deductedValue;
    let actualMaterial: number = totalweight - core;
    this.data.comparisonSecondValue = Number(actualMaterial.toFixed(10));
  }

  ChangedDeductedValue(e) {
    let core: number = e.value;
    let totalweight: number = Number(this.data.totalweight);
    let actualMaterial: number = Number(totalweight - core)
    this.data.comparisonSecondValue = Number(actualMaterial.toFixed(10));
  }

  setMinThreshold(e) {
    this.stockService.GetMinThreshold(e.value).subscribe(data => {
      if (data != null) {
        this.data.minThreshold = data;
      } else {
        this.data.minThreshold = 0;
      }
    });
  }

  setCurrency(e) {
    if (e.value) {
      this.stockService.GetCurrency(e.value).subscribe(data => {
        if (data != null) {
          this.data.currency = data["iso"];
          this.currency = data["iso"];
        }
      }, subError => {
        notify(subError.error, 'error', 5000);
      });
    }
  }

  stockCategory(e) {
    var oldCodeNot = this.data.code;

    if (oldCodeNot && oldCodeNot != "REC") {
      this.oldCode = oldCodeNot;
    }

    if (e.value == EnumRecipe) {
      this.isRecipe = true;
      this.data.stockCategoryId = e.value;
      this.data.code = "REC";
    } else {
      this.isRecipe = false;
      this.data.stockCategoryId = e.value;
      this.data.code = this.oldCode;
    }
  }
}
