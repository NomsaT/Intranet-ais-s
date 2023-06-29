import { Component, OnInit } from '@angular/core';
import { DashboardService } from 'src/app/core/services/dashboard.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { ApexAxisChartSeries, ApexDataLabels, ApexNonAxisChartSeries, ApexResponsive,ApexChart,ApexFill, ApexMarkers, ApexTitleSubtitle, ApexTooltip, ApexXAxis, ApexYAxis, ChartComponent, NgApexchartsModule } from "ng-apexcharts";
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/core/services/auth.service';

export type ChartOptions = {
  series: ApexNonAxisChartSeries;
  chart: ApexChart;
  dataLabels: ApexDataLabels;
  responsive: ApexResponsive[];
  labels: any;
  background: any;
};

@Component({
  selector: 'app-production-dashboard',
  templateUrl: './production-dashboard.component.html',
  styleUrls: ['./production-dashboard.component.scss']
})

export class ProductionDashboardComponent implements OnInit {
  monthlyProductionValueObj: any ;
  monthlyProductionValue: any;
  public chartOptions: Partial<ChartOptions>;
  pieLabels: string[] = [];
  pieTotals: number[] = [];
  allowViewAdded = false;

  productionStockItems: any = [];

  productionStockObj: any = [];
  productionStockListExist: boolean = false; // caters for non  production and non-production stock
  constructor(private dashboardService: DashboardService,private router: Router, private authService: AuthenticationService) { 
    this.dashboardService.getProductuctionStoreValue().subscribe(ProductuctionStoreValue => {
      this.monthlyProductionValueObj =GlobalMethodsService.formatCurrency(ProductuctionStoreValue, "R");
    });

    //117 Permission for the stock dashboard
    this.allowViewAdded = this.authService.HavePermission(117);

    //Get's a sum of Non Production and Production Stock
    this.dashboardService.getProductionStock().subscribe(ProductionStock => {
      this.productionStockListExist = ProductionStock.length != 0 ? true : false;
      ProductionStock.forEach(item => {
        if(item.isInProductionStore)
        {
            this.pieLabels.push("Production Stock" );
        }
        else
        {
          this.pieLabels.push("Non Production Stock");
        }
        this.pieTotals.push(item.price);
        if (item.price != undefined) {
          item.price = GlobalMethodsService.formatCurrency(item.price, "R");
        }
      });
      this.loadPieChart();
      this.productionStockObj = ProductionStock;
    });

    //Gets a list of products in Production store
    this.dashboardService.getProductionStockItems().subscribe(
       productionStockItems => {
        this.productionStockListExist = productionStockItems.length != 0 ? true : false;
        if (this.productionStockListExist) {
          productionStockItems.splice(6);//get the first six elements in the list
          productionStockItems.forEach(item => {
          this.productionStockItems.push(item)
         });
        }

       
      });
    
  }
  ngOnInit(): void {
  }


  navigateProductionStock() {
    this.router.navigate([`/production-stock`]);
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
        offsetX: 25,
      },
      chart: {
        width: 500,
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



}
