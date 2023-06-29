import { formatCurrency } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import notify from 'devextreme/ui/notify';
import { internalOrderAction } from '../../../core/models/internalOrderAction.models';
import { AuthenticationService } from '../../../core/services/auth.service';
import { DashboardService } from '../../../core/services/dashboard.service';
import { EventService } from '../../../core/services/event.service';
import { GlobalMethodsService } from '../../../core/services/global-methods.service';
import { OrdersService } from '../../../core/services/orders.service';
import { ApexChart, ApexNonAxisChartSeries, ApexResponsive, ChartComponent } from "ng-apexcharts";
import { ChartType } from './dashboard.model';
export type ChartOptions = {
  series: ApexNonAxisChartSeries;
  chart: ApexChart;
  responsive: ApexResponsive[];
  labels: any;
};


@Component({
  selector: 'app-default',
  templateUrl: './default.component.html',
  styleUrls: ['./default.component.scss']
})

export class DefaultComponent implements OnInit {
  internalOrderAction: internalOrderAction = new internalOrderAction();
  isVisible: string;
  birthdayObj: any = [];
  ordersObj: any = [];
  allOrders: any = [];
  ordersObjReview: any = [];
  flaggedItemsObj: any = [];
  stockItemsObj: any = [];
  totalStockItemsObj: any = [];
  totalDepartmentObj: any = [];
  totalLocationsObj: any = [];
  monthlyStockValueObj: any = [];
  PriceLookUpBadgeObj: any = [];
  PrintingBadgeObj: any = [];
  minStockThresholdObj: any = [];
  departmentStockObj: any = [];
  emailSentBarChart: ChartType;
  monthlyEarningChart: ChartType;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  popupVisibleIO = false;
  loadingVisible = false;
  selectedOrder: any;
  IOTitle: any;
  filterForMe = false;
  filterForMeReview = false;
  filterConvert: any;
  filterConvertReview: any;
  userId: any;
  orderResult: any;
  IOId: any;
  scrollvalue = "never";
  nativevalue = false;
  popupVisibleCommentIO = false;
  popupForAlreadyApprovedOrder = false;
  nonApprovedItemid: number;
  approvedByName: string;
  CommentTitle: any;
  PopUpTitle: any;
  IOAction: any;
  ApproverLoggedIn = true;
  InternalComment: any;
  orderId: any;
  stocktakeObj: any = [];

  buttonDeny: any = {
    text: "Deny",
    type: "danger",
    useSubmitBehavior: true,
    onClick: () => {
      this.IOAction = "Deny";
      this.CommentTitle = "Why is the Internal Order being denied?";
      this.popupVisibleCommentIO = true;
    }
  }

  buttonReview: any = {
    text: "Review",
    type: "default",
    useSubmitBehavior: true,
    onClick: () => {
      this.IOAction = "Review";
      this.CommentTitle = "Why is the Internal Order marked for review?";
      this.popupVisibleCommentIO = true;
    }
  }

  buttonApprove: any = {
    text: "Approve",
    type: "success",
    useSubmitBehavior: true,
    onClick: () => {
      this.loadingVisible = true;
      this.internalOrderAction.id = this.IOId;
      this.internalOrderAction.internalComment = "Approved";
      this.orderService.editApproveOrder(this.internalOrderAction).subscribe(Orders => {
        this.orderResult = Orders;
        if (this.orderResult == 1) {
          notify("Internal Order Approved and Email sent", 'success', 5000);

          this.filterConvert = localStorage.getItem("Filter");
          if (this.filterConvert === "true") {
            //filter approve by for person logged in
            this.filterForMe = true;
            this.getFilteredOrders();
          } else {
            //show all
            this.getOrders();
          }

          this.filterConvertReview = localStorage.getItem("FilterReview");
          if (this.filterConvertReview === "true") {
            //filter approve by for person logged in
            this.filterForMeReview = true;
            this.getFilteredOrdersReview();
          } else {
            //show all
            this.getOrdersReview();
          }

          this.loadingVisible = false;
          this.popupVisibleIO = false;
          this.internalOrderAction = new internalOrderAction();
        } else {
          this.loadingVisible = false;
          this.internalOrderAction = new internalOrderAction();
          notify("Internal Order not Approved", 'error', 5000);
        }
      });
    }
  }

  buttonOK: any = {
    text: "Submit",
    type: "success",
    useSubmitBehavior: true,
    onClick: () => {
      this.loadingVisible = true;
      this.internalOrderAction.id = this.IOId;
      if (this.IOAction == "Deny") {
        this.orderService.editDenyOrder(this.internalOrderAction).subscribe(Orders => {
          this.orderResult = Orders;
          if (this.orderResult == 3) {
            notify("Internal Order Denied", 'success', 5000);

            this.filterConvert = localStorage.getItem("Filter");
            if (this.filterConvert === "true") {
              //filter approve by for person logged in
              this.filterForMe = true;
              this.getFilteredOrders();
            } else {
              //show all
              this.getOrders();
            }

            this.filterConvertReview = localStorage.getItem("FilterReview");
            if (this.filterConvertReview === "true") {
              //filter approve by for person logged in
              this.filterForMeReview = true;
              this.getFilteredOrdersReview();
            } else {
              //show all
              this.getOrdersReview();
            }

            this.loadingVisible = false;
            this.popupVisibleIO = false;
            this.internalOrderAction = new internalOrderAction();
          } else {
            this.loadingVisible = false;
            this.internalOrderAction = new internalOrderAction();
            notify("Internal Order not Denied", 'error', 5000);
          }
        });
      } else if (this.IOAction == "Review") {
        this.orderService.editReviewOrder(this.internalOrderAction).subscribe(Orders => {
          this.orderResult = Orders;
          if (this.orderResult == 5) {
            notify("Internal Order is being reviewed - PENDING", 'success', 5000);

            this.filterConvert = localStorage.getItem("Filter");
            if (this.filterConvert === "true") {
              //filter approve by for person logged in
              this.filterForMe = true;
              this.getFilteredOrders();
            } else {
              //show all
              this.getOrders();
            }

            this.filterConvertReview = localStorage.getItem("FilterReview");
            if (this.filterConvertReview === "true") {
              //filter approve by for person logged in
              this.filterForMeReview = true;
              this.getFilteredOrdersReview();
            } else {
              //show all
              this.getOrdersReview();
            }

            this.loadingVisible = false;
            this.popupVisibleIO = false;
            this.internalOrderAction = new internalOrderAction();
          } else {
            this.loadingVisible = false;
            this.internalOrderAction = new internalOrderAction();
            notify("Internal Order is not being reviewed - PENDING", 'error', 5000);
          }
        });
      }

      this.IOAction = "";
      this.popupVisibleCommentIO = false;
    }
  }

  buttonCancel: any = {
    text: "Cancel",
    type: "danger",
    useSubmitBehavior: true,
    onClick: () => {
      this.popupVisibleCommentIO = false;
    }
  }


  buttonClose: any = {
    text: "Close",
    type: "normal",
    useSubmitBehavior: true,
    onClick: () => {
      this.popupForAlreadyApprovedOrder = false;
    }
  }
  @ViewChild('content') content;
  allowViewStockTake = false;
  allowViewAdded = false;
  allowViewDepartment = false;
  allowViewPrice = false;

  constructor(private modalService: NgbModal, private eventService: EventService, private authService: AuthenticationService, private dashboardService: DashboardService, private orderService: OrdersService, private router: Router, private route: ActivatedRoute, private globalMethodsService: GlobalMethodsService) {
    this.ApproverLoggedIn = true;
    this.authService.currentUserSubject.subscribe(user => {
      if (user) {
        this.userId = user.id
      }
    })

    this.authService.SetSite("acs");
    //Permissions
    this.allowViewStockTake = this.authService.HavePermission(106);
    this.allowViewAdded = this.authService.HavePermission(12);
    this.allowViewDepartment = this.authService.HavePermission(8);
    this.allowViewPrice = this.authService.HavePermission(18);

    this.filterConvert = localStorage.getItem("Filter");
    if (this.filterConvert === "true") {
      //filter approve by for person logged in
      this.filterForMe = true;
      this.getFilteredOrders();
    } else {
      //show all
      this.getOrders();
    }

    this.filterConvertReview = localStorage.getItem("FilterReview");
    if (this.filterConvertReview === "true") {
      //filter approve by for person logged in
      this.filterForMeReview = true;
      this.getFilteredOrdersReview();
    } else {
      //show all
      this.getOrdersReview();
    }

    this.dashboardService.getBirthdays().subscribe(Birthdays => {
      this.birthdayObj = Birthdays;
    });

    this.getPriceLookupBadgeContent();
    this.getPrintingBadgeContent();
    this.dashboardService.getWeeklyStockItems().subscribe(StockItems => this.stockItemsObj = StockItems);
    this.dashboardService.getTotalStockItems().subscribe(TotalStockItems => this.totalStockItemsObj = TotalStockItems);

    this.dashboardService.getMonthlyStockValue().subscribe(MonthlyStockValue => {

      this.monthlyStockValueObj = GlobalMethodsService.formatCurrency(MonthlyStockValue, "R");

    });

    this.dashboardService.getDepartments().subscribe(TotalDepartments => this.totalDepartmentObj = TotalDepartments);
    this.dashboardService.getLocations().subscribe(TotalLocations => this.totalLocationsObj = TotalLocations);
    this.dashboardService.getMinStockThreshold().subscribe(MinStockThreshold => this.minStockThresholdObj = MinStockThreshold);

    this.dashboardService.getDepartmentStock().subscribe(DepartmentStock => {
      DepartmentStock.forEach(function (value) {
        if (value.totalPrice != undefined) {
          value.totalPrice = GlobalMethodsService.formatCurrency(value.totalPrice, "R");
        }
      });
      this.departmentStockObj = DepartmentStock;
    });

    setInterval(() => {
      this.getPriceLookupBadgeContent();
      this.getPrintingBadgeContent();
    }, globalMethodsService.ONE_MINUTE_IN_MILLIS);

    this.dashboardService.getStocktake().subscribe(Stocktake => this.stocktakeObj = Stocktake);
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
    this.dashboardService.getOrders().subscribe(Orders => {
      this.ordersObj = Orders;
    })
  }

  ngAfterViewInit() {
    var showOrder = null;
    this.dashboardService.getOrders().subscribe(Orders => {
      this.ordersObj = Orders;
    });
    this.dashboardService.getAllOrders().subscribe(Orders => {
      this.allOrders = Orders;
      var isReport = false;
      var showPopup = false;
      var showReviewOrder = false;
      var itemid;
      var approveByFullName = "";
      var message = "";
      this.route.queryParams.subscribe(params => {
        this.allOrders.forEach(function (item) {
          if (params.orderId == item.id) {
            switch (item.statusId) {
              case 1: {
                message = " has already been approved by";
                showPopup = true;
                itemid = item.id;
                approveByFullName = item.approveByFullName.replace("-", " ");
                break;
              }
              case 2: {
                isReport = true;
                showOrder = item;
                break;
              }
              case 5: {
                showReviewOrder = true;
                showOrder = item;
                break;
              }
              default: {
                this.router.navigate(["/error"], { queryParams: {} });
                break;
              }
            }
          }
        });
        if (params.orderId && !(showReviewOrder || isReport || showPopup)) {
          this.router.navigate(["error"], { queryParams: {} });
          return;
        }
      });
      if (isReport) {
        this.openInternalOrder(showOrder);
        isReport = false;
      }
      if (showPopup) {
        this.popupForAlreadyApprovedOrder = true;
        this.PopUpTitle = message;
        this.nonApprovedItemid = itemid;
        this.approvedByName = approveByFullName;
        showPopup = false;
      }
      if (showReviewOrder) {
        this.openInternalOrderReview(showOrder)
      }
    });
  }

  changeLayout(layout: string) {
    this.eventService.broadcast('changeLayout', layout);
  }

  navigate() {
    this.router.navigate([`/price-lookup`]);
  }

  navigateStock() {
    this.router.navigate([`/stocklist`]);
  }

  navigateOrders() {
    this.router.navigate([`/orders`]);
  }

  navigateDepartment() {
    this.router.navigate([`/department`]);
  }

  nativagateStocktake() {
    this.router.navigate([`/stocktake`]);
  }

  getLowStockObj(obj) {
    this.navigateLowStock(obj.code, obj.productName);
  }

  navigateLowStock(code, productName) {
    this.router.navigate([`/stocklist`], { queryParams: { code: code, productName: productName }, relativeTo: this.route });
  }

  openInternalOrderReview(order) {
    this.router.navigate(['/orders-modify'], { state: { id: order.id, action: 2 } });
  }

  openInternalOrder(order) {
    this.selectedOrder = order;
    this.selectedOrder.TotalDisplay = this.selectedOrder.total + this.selectedOrder.vat;
    this.IOTitle = "Approve Internal Order #" + order.id;
    this.IOId = order.id;

    if (this.userId == order.approveById) {
      this.ApproverLoggedIn = true;
    } else {
      this.ApproverLoggedIn = false;
    }

    this.popupVisibleIO = true;
  }

  getOrders() {
    this.dashboardService.getOrders().subscribe(Orders => {
      this.ordersObj = Orders;
      //Currency Format
      var total = 0;
      var IO = undefined;
      var OOI = undefined;
      var services = undefined;

      Orders.forEach(function (value) {
        if (value.total != undefined) {
          value.TotalDisplay = value.total + value.vat;
          value.total = GlobalMethodsService.formatCurrency(value.total, " ");
          value.vat = GlobalMethodsService.formatCurrency(value.vat, " ");
          value.TotalDisplay = GlobalMethodsService.formatCurrency(value.TotalDisplay, " ");
        }
        IO = value.internalOrderItems;
        IO.forEach(function (value) {
          value.originalValue = GlobalMethodsService.formatCurrency(value.originalValue, " ");
          value.value = GlobalMethodsService.formatCurrency(value.value, " ");
          value.total = GlobalMethodsService.formatCurrency(value.total, " ");
        })
        OOI = value.onceOffItems;
        OOI.forEach(function (value) {
          value.originalValue = GlobalMethodsService.formatCurrency(value.originalValue, " ");
          value.value = GlobalMethodsService.formatCurrency(value.value, " ");
          value.total = GlobalMethodsService.formatCurrency(value.total, " ");
        })
        services = value.services;
        services.forEach(function (value) {
          value.originalValue = GlobalMethodsService.formatCurrency(value.originalValue, " ");
          value.value = GlobalMethodsService.formatCurrency(value.value, " ");
          value.total = GlobalMethodsService.formatCurrency(value.total, " ");
        })
      })
    });
  }
  getFilteredOrders() {
    this.dashboardService.getFilteredOrders(this.userId).subscribe(Orders => {
      this.ordersObj = Orders;
      //this.getOrders();
      //Currency Format
      var total = 0;
      var IO = undefined;
      var OOI = undefined;
      var services = undefined;

      Orders.forEach(function (value) {
        if (value.total != undefined) {
          value.TotalDisplay = value.total + value.vat;
          value.total = GlobalMethodsService.formatCurrency(value.total, " ");
          value.vat = GlobalMethodsService.formatCurrency(value.vat, " ");
          value.TotalDisplay = GlobalMethodsService.formatCurrency(value.TotalDisplay, " ");
        }
        IO = value.internalOrderItems;
        IO.forEach(function (value) {
          value.originalValue = GlobalMethodsService.formatCurrency(value.originalValue, " ");
          value.value = GlobalMethodsService.formatCurrency(value.value, " ");
          value.total = GlobalMethodsService.formatCurrency(value.total, " ");
        })
        OOI = value.onceOffItems;
        OOI.forEach(function (value) {
          value.originalValue = GlobalMethodsService.formatCurrency(value.originalValue, " ");
          value.value = GlobalMethodsService.formatCurrency(value.value, " ");
          value.total = GlobalMethodsService.formatCurrency(value.total, " ");
        })
        services = value.services;
        services.forEach(function (value) {
          value.originalValue = GlobalMethodsService.formatCurrency(value.originalValue, " ");
          value.value = GlobalMethodsService.formatCurrency(value.value, " ");
          value.total = GlobalMethodsService.formatCurrency(value.total, " ");
        })
      })
    });
  }

  FilterForMe(e) {
    if (e.value === true) {
      //filter approve by for person logged in
      localStorage.setItem("Filter", e.value);
      this.getFilteredOrders();
    } else {
      //show all
      localStorage.setItem("Filter", e.value);
      this.getOrders();
    }
  }

  getOrdersReview() {
    this.dashboardService.getOrdersReview().subscribe(Orders => {
      this.ordersObjReview = Orders;
    });
  }

  getFilteredOrdersReview() {
    this.dashboardService.getFilteredOrdersReview(this.userId).subscribe(Orders => {
      this.ordersObjReview = Orders;
    });
  }

  FilterForMeReview(e) {
    if (e.value === true) {
      //filter approve by for person logged in
      localStorage.setItem("FilterReview", e.value);
      this.getFilteredOrdersReview();
    } else {
      //show all
      localStorage.setItem("FilterReview", e.value);
      this.getOrdersReview();
    }
  }

  //#region Get methods

  getPriceLookupBadgeContent() {

    this.dashboardService.getFlaggedItems().subscribe(FlaggedItems => this.flaggedItemsObj = FlaggedItems);

    this.dashboardService.getPriceLookupBadge().subscribe(PriceLookUpBadge => {

      this.PriceLookUpBadgeObj = PriceLookUpBadge;
      this.globalMethodsService.priceLookBadgeRefresh(true);

    });
  }

  getPrintingBadgeContent() {

    this.dashboardService.getPrintingBadge().subscribe(PrintingBadge => {

      this.PrintingBadgeObj = PrintingBadge;
      this.globalMethodsService.printingBadgeRefresh(true);

    });
  }

  // This function can be used if we want to implement the class-(moving icon) based on "Date" & NOT "Last-Updated:   [ngClass]="{'bx-fade-right': newFlagItem(FlaggedItems)}""
  /*  newFlagItem(FlaggedItems: any) {

      let currentDate = new Date();
      let itemDate = new Date(FlaggedItems.date);

      let diff = currentDate.getTime() - itemDate.getTime();

      return diff > 500;
    }*/

  //#endregion
}
