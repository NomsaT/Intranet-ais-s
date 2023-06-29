import { Component, Inject, OnInit, ViewChild } from "@angular/core";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { DxDataGridComponent } from "devextreme-angular";
import { AuthenticationService } from "../../core/services/auth.service";
import { GlobalMethodsService } from "../../core/services/global-methods.service";
import { ChartService } from "../../core/services/chart.service";
import { OrdersService } from "../../core/services/orders.service";
import {
  arrModel,
  BasicColumnChart,
  Quarter,
  monthNames,
  Month,
} from "./basicColumnChart";
import * as moment from "moment";
import { internalOrder } from "../../core/models/internalOrder.models";
import { internalOrderItems } from "../../core/models/internalOrderItems.models";
import {
  arrayModel,
  StackedBarChart,
  departmentNames,
} from "./stackedBarChart";
import { departmentStock } from "../../core/models/departmentStock.models";
import { LineChart } from "./lineChart";
import { ChartComponent } from "ng-apexcharts";

@Component({
  selector: "app-charts",
  templateUrl: "./charts.component.html",
  styleUrls: ["./charts.component.scss"],
})
export class ChartsComponent implements OnInit {
  @ViewChild("content") content;
  @ViewChild("orders", { static: false }) grid: DxDataGridComponent;
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  pageName: string = "Chart";
  event: any;
  allowModify = false;                                                                
  ordersObj: any;
  basicColumnChart: BasicColumnChart;
  stackedBarChart: StackedBarChart;
  lineChart = LineChart;                                   
                                                           
  xAxis: any = [];
  chartData: boolean = false;
  amount: any;
  monthValues = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
  departmentNames: [];
  monthNames = monthNames;
                                                                                                                                                                     
  quarters: Quarter[] = [...Array(4).keys()].map(
    (value, i) => new Quarter(i + 1, value * 3 + 1, (i + 1) * 3)
  );

  q1: number[] = [];
  q2: number[] = [];                                   
  q3: number[] = [];
  q4: number[] = [];
  stockObj: any;
  depNames: any[] = [];
  depTotals: any[] = [];

  countDep = 1;
  orders: internalOrder[] = [];
  totalProfit = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
  totalSpend = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];

  constructor(
    @Inject("BASE_URL") baseUrl: string,
    private modalService: NgbModal,
    private globalMethodsService: GlobalMethodsService,
    private authService: AuthenticationService,
    private chartService: ChartService,
    private orderService: OrdersService
  ) {
    this.allowModify = this.authService.HavePermission(101);
  }

  getOrders() {
    this.chartService.getOrders().subscribe((Orders) => {
      this.ordersObj = Orders;
      this.orders = Orders;
      this.loadLineChartData();
    });
  }
  getStock(o) {
    let sum = 0;
    //grouping
    this.departmentNames.forEach((d, index) => {
      this.depGroup(o).filter((a, i) => {
        if (a.department === d) {
          sum += a.value;
          this.countDep += 1;

          this.depNames[index] = this.countDep;
        } else {
          sum = 0;
          this.countDep = 0;
        }
      });
      this.depGroup(o.filter((acc) => acc.grnNumber > 0)).filter((a) => {
        if (a.department === d) {
          sum += a.value;
          this.depTotals[index] = sum;
        } else {
          sum = 0;
        }
      });
    });
    //ending
  }

  ngOnInit() {
    this.loadDep();
    this.loadOrders();
    this.getOrders();

    this.basicColumnChart = new BasicColumnChart();
    this.stackedBarChart = new StackedBarChart();
    /*this.loadChart();*/

    const attribute = document.body.getAttribute("data-layout");

    this.isVisible = attribute;
    const vertical = document.getElementById("layout-vertical");
    if (vertical != null) {
      vertical.setAttribute("checked", "true");
    }
    if (attribute == "horizontal") {
      const horizontal = document.getElementById("layout-horizontal");
      if (horizontal != null) {
        horizontal.setAttribute("checked", "true");
      }
    }
  }
//Need this 
  loadLineChartData() {
    const monthsActuals = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
    let countInMonths = 1;
    let sum = 0;

    const grouped = this.groupByMonth(this.orders);
    this.monthNames.forEach((m, monthIndex) => {
      grouped.filter((a, i) => {
        if (a.month === m) {
          sum += a.value;
          countInMonths += 1;

          this.monthValues[monthIndex] = sum;
          monthsActuals[monthIndex] = countInMonths;
        } else {
          sum = 0;
          countInMonths = 0;
        }
      });

      const groupApproved = this.groupByMonth(
        this.orders.filter((acc) => acc.dateApproved != null)
      );

      groupApproved.filter((a) => {
        if (a.month === m) {
          sum += a.value;
          this.totalSpend[monthIndex] = sum;
        } else {
          sum = 0;
        }
      });
    });

    this.lineChart.series = [
      {
        data: this.monthValues,
        name: "Total Spend Amount",
      },
    ];
  }
  //Do not need 

  depGroup(orders: departmentStock[]) {
    let arr: arrayModel[] = [];

    orders.forEach((er) => {
      if (arr.filter((x) => x.department != er.departmentName)) {
        const model: arrayModel = {
          department: er.departmentName,
          value: er.total,
        };
        arr.push(model);
      }
    });

    return arr;
  }

  groupByMonth(orders: internalOrder[]) {
    let arr: arrModel[] = [];
    orders.forEach((er) => {
      if (this.monthNames[moment(er.dateApproved).month()]) {
        const model: arrModel = {
          month: this.monthNames[moment(er.dateApproved).month()],
          value: this.getValue(er.internalOrderItems),
        };
        arr.push(model);
      }
    });
    return arr;
  }
  //Need this

  loadOrders() {
    this.chartService.getOrders().subscribe((Orders) => {
      this.orders = Orders;
      this.loadLineChartData();
      Orders.forEach((order) => {
        this.getQuarter(order.dateApproved, order);
      });

      let sum1 = this.q1.reduce((a, b) => a + b, 0).toFixed(0);
      let sum2 = this.q2.reduce((a, b) => a + b, 0).toFixed(0);
      let sum3 = this.q3.reduce((a, b) => a + b, 0).toFixed(0);
      let sum4 = this.q4.reduce((a, b) => a + b, 0).toFixed(0);
//do not 
      this.chartData = true;
      this.basicColumnChart.series = [
        {
          data: [sum1, sum2, sum3, sum4],
          name: "Total Amount",
        },
      ];
    });
  }
  //do not
  groupDepartments(test: departmentStock[]) {
    var res = test.reduce((acc, obj) => {
      var existItem = acc.find(
        (item) => item.departmentName === obj.departmentName
      );
      if (existItem) {
        existItem.total += obj.total;
        return acc;
      }
      acc.push(obj);
      return acc;
    }, []);

    return res;
  }
  //do not 

  loadDep() {
    this.chartService.getDepartmentStock().subscribe((Stocks) => {
      this.groupDepartments(Stocks).forEach((stock) => {
        this.depNames.push(stock.departmentName);
        this.depTotals.push(stock.total.toFixed(0));

        this.stackedBarChart.series = [
          {
            name: "Total",
            data: this.depTotals,
          },
        ];
        this.stackedBarChart.xaxis = {
          title: {
            text: "Profit Centers/Departments",          },
          categories: this.depNames,
        };
      });
    });
  }
//do not 
  getQuarter(date?: Date, order?: internalOrder) {
    if (moment(date).quarter() == 1) {
      this.q1.push(this.getValue(order.internalOrderItems));
      return order;
    } else if (moment(date).quarter() == 2) {
      this.q2.push(this.getValue(order.internalOrderItems));
      return order;
    } else if (moment(date).quarter() == 3) {
      this.q3.push(this.getValue(order.internalOrderItems));
      return order;
    } else if (moment(date).quarter() == 4) {
      this.q4.push(this.getValue(order.internalOrderItems));
      return order;
    }
  }
  getValue(internalOrderItems: internalOrderItems[]): number {
    return internalOrderItems.map((v) => v.total).reduce((a, b) => a + b, 0);
  }
}
