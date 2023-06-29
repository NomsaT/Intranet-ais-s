import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DxDataGridComponent } from 'devextreme-angular';
import * as moment from 'moment';
import { internalOrder } from 'src/app/core/models/internalOrder.models';
import { ChartService } from 'src/app/core/services/chart.service';
import { Service, ComplaintsWithPercent } from 'src/app/core/services/app.service';
import { DashboardService } from '../../core/services/dashboard.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';


import { LineChart, monthNames } from "./lineChart";
import { ApexAxisChartSeries, ApexDataLabels, ApexFill, ApexMarkers, ApexTitleSubtitle, ApexTooltip, ApexXAxis, ApexYAxis, ChartComponent, NgApexchartsModule } from "ng-apexcharts";



import {
  ApexNonAxisChartSeries,
  ApexResponsive,
  ApexChart
} from "ng-apexcharts";
import { plugins } from 'chart.js';
import { DepartmentContribution, PurchaseValue } from './fin-dashboard.model';
import { arrModel } from '../charts/basicColumnChart';

export type ChartOptions = {
  series: ApexNonAxisChartSeries;
  chart: ApexChart;
  colors: any[];
  dataLabels: ApexDataLabels;
  responsive: ApexResponsive[];
  labels: any;
  background: any;
};
export type ChartOption = {
  series: ApexNonAxisChartSeries;
  chart: ApexChart;

  responsive: ApexResponsive[];
  labels: any;
};


@Component({
  selector: 'app-fin-dashboard',
  providers: [Service],
  templateUrl: './fin-dashboard.component.html',
  styleUrls: ['./fin-dashboard.component.scss']
})
export class FinDashboardComponent implements OnInit {
  @ViewChild("content") content;
  @ViewChild("orders", { static: false }) grid: DxDataGridComponent;
  public chartOptions: Partial<ChartOptions>;
  dataSource: PurchaseValue[] | DepartmentContribution[];
  public chartOption: Partial<ChartOptions>;
  public series: ApexAxisChartSeries;
  public dataLabels: ApexDataLabels;
  public markers: ApexMarkers;
  public chart: ApexChart;
  public options: ApexChart
  public fill: ApexFill;
  public yaxis: ApexYAxis;
  public xaxis: ApexXAxis;
  public tooltip: ApexTooltip;
  departmentStockObj: any = [];
  monthlyStockValueObj: any = [];
  monthlyStockValue: any;
  monthlyStockPercentage: number;
  newStock: any;
  recievedStock: any;
  xAxis: any = [];
  chartData: boolean = false;
  amount: any;
  lineValues = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
  columnValues = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];

  lineColumnChart = LineChart;

  departmentNames: [];
  depNames: any[] = [];
  depTotals: any[] = [];
  monthNames = monthNames;
  countDep = 1;
  backgroundColor: any;
  title: { text: string; align: string; };
  IOApproved = 70;
  IOPending = 5;
  IOPendingPriceApproval = 5;
  IOReview = 5;
  IODraft = 15;
  pieLabels: string[] = [];
  pieTotals: number[] = [];
  orderListExist: boolean = false;
  departmentStockListExist: boolean = false;
  lineSeries: number[] = [];
  columnSeries: number[] = [];

  chartOptions2: { palette: string; title: string; argumentAxis: { label: { overlappingBehavior: string; }; }; tooltip: { enabled: boolean; shared: boolean; customizeTooltip(info: any): { html: string; }; }; valueAxis: ({ name: string; position: string; tickInterval: number; showZero?: undefined; label?: undefined; constantLines?: undefined; valueMarginsEnabled?: undefined; } | { name: string; position: string; showZero: boolean; label: { customizeText(info: any): string; }; constantLines: { value: number; color: string; dashStyle: string; width: number; label: { visible: boolean; }; }[]; tickInterval: number; valueMarginsEnabled: boolean; })[]; commonSeriesSettings: { argumentField: string; }; series: { type: string; valueField: string; axis: string; name: string; color: string; }[]; legend: { verticalAlignment: string; horizontalAlignment: string; }; };

  constructor(service: Service, private dashboardService: DashboardService, private router: Router,
    private route: ActivatedRoute, private globalMethodsService: GlobalMethodsService, private chartService: ChartService) {

    this.loadChartData();

    this.chartOption = {
      series: [0, 0, 0, 0, 0],
      dataLabels: {
        enabled: true,
      },
      chart: {
        width: 500,
        type: "donut",
        offsetX: 200,
      },

      labels: ["Total orders pending approval", "Total orders approved", "Total orders pending price approval"
        , "Total orders review", "Total orders draft"
      ],


      responsive: [
        {
          breakpoint: 480,
          options: {
            chart: {
              width: 100
            },
            legend: {
              position: "bottom"
            }
          }
        }
      ]
    }

    this.dashboardService.getDonutData().subscribe(results => {
      
      var sum = 0;
      results.forEach(x => sum += x);
      this.orderListExist = results.length && sum>0 ? true : false;

      this.chartOption = {
        series: results,
        dataLabels: {
          enabled: true,
          formatter(value: any, opts: any): any {
            return opts.w.config.series[opts.seriesIndex];
          },
        },
        chart: {
          width: 500,
          type: "donut",
          offsetX: 200,
         
        },
        colors: ['#f1b44c', '#34c38f', '#ff2d53', '#337ab7', '#556ee6', '#f46a6a'],
        labels: ["Total orders pending approval", "Total orders approved", "Total orders pending price approval"
          , "Total orders review", "Total orders draft","Total orders closed"
        ],
        responsive: [
          {
            breakpoint: 480,
            options: {
              chart: {
                width: 100
              },
              legend: {
                position: "bottom"
              }
            }
          }
        ],
      };
    })



    this.dashboardService.getMonthlyStockValue().subscribe(MonthlyStockValue => {

      this.monthlyStockValueObj = GlobalMethodsService.formatCurrency(MonthlyStockValue, "R");

    });
    this.dashboardService.getMonthlyStockPercentage().subscribe(monthlyStockPercentage => {
      this.monthlyStockPercentage = Number.parseInt(monthlyStockPercentage.toString());
      this.monthlyStockValue = Math.abs(parseFloat(monthlyStockPercentage.toString())) + "%";
    });
    this.dashboardService.getDepartmentStock().subscribe(DepartmentStock => {
      this.departmentStockListExist = DepartmentStock.length != 0 ? true : false;
      DepartmentStock.forEach(item => {
        this.pieLabels.push(item.name);
        this.pieTotals.push(item.totalPrice);

        if (item.totalPrice != undefined) {
          item.totalPrice = GlobalMethodsService.formatCurrency(item.totalPrice, "R");
        }
      });

      this.loadPieChart();
      this.departmentStockObj = DepartmentStock;
    });

  }

  loadPieChart() {
    this.chartOptions = {
      series: this.pieTotals,
      dataLabels: {
        enabled: true,
        //Formatting the numbers in Pie
        //Spaces between numbers - Anelisa
        formatter(value: any, opts: any): any {
          return "R" + opts.w.config.series[opts.seriesIndex].toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ");
        },
      },
      chart: {
        width: 393,
        type: "pie",
        offsetX: 200,
      },
      labels: this.pieLabels,
      responsive: [
        {
          breakpoint: 480,
          options: {
            chart: {
              width: 200
            },
            legend: {
              position: "bottom"
            }

          }
        }
      ]
    };
  }

  loadChartData() {
    const monthsActuals = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
    let countInMonths = 1;

    this.dashboardService.getDepartmentContribution().subscribe(results => {
      this.columnSeries = this.group(results).map(x => x.value);

      this.monthNames.forEach((m, monthIndex) => {
        results.filter((a, i) => {
          if (a.month === m) {

            countInMonths += 1;

            this.columnValues[monthIndex] = a.value;
            monthsActuals[monthIndex] = countInMonths;
          } else {
            countInMonths = 0;
          }
        });

      });
    });

    this.dashboardService.getPurchaseValue().subscribe(results => {
      this.lineSeries = this.groupData(results).map(x => x.value);
      this.monthNames.forEach((m, monthIndex) => {
        results.filter((a, i) => {
          if (a.month === m) {
            countInMonths += 1;

            this.lineValues[monthIndex] = a.value;
            monthsActuals[monthIndex] = countInMonths;
          } else {
            countInMonths = 0;
          }
        });
        this.lineColumnChart = {
          ...this.lineColumnChart,
          series: [
            {
              name: "Total Department Contribution Value",
              type: "column",
              data: this.columnValues
            },
            {
              name: "Total Purchase Value",
              type: "line",
              data: this.lineValues
            }
          ]
        };
      });
    })
  }

  group(purchase: DepartmentContribution[]) {
    let arr: DepartmentContribution[] = [];
    purchase.forEach((x) => {
      if (this.monthNames.find(m => m == x.month)) {
        const model = {
          month: x.month,
          value: x.value
        } as DepartmentContribution;

        arr.push(model);
      }
    });
    return arr;
  }
  groupData(purchase: PurchaseValue[]) {
    let arr: PurchaseValue[] = [];
    purchase.forEach((x) => {
      if (this.monthNames.find(m => m == x.month)) {
        const model = {
          month: x.month,
          value: x.value,
        };

        arr.push(model);
      }
    });
    return arr;
  }

  ngOnInit(): any {
  }
}
