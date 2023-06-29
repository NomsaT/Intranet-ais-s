import { ApexAxisChartSeries, ApexChart, ApexXAxis, ApexYAxis, ApexTitleSubtitle, ApexFill, ApexTooltip } from "ng-apexcharts";

export type ChartType = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  xaxis: ApexXAxis;
  yaxis: ApexYAxis | ApexYAxis[];
  title: ApexTitleSubtitle;
  labels: string[];
  stroke: any; // ApexStroke;
  dataLabels: any; // ApexDataLabels;
  fill: ApexFill;
  tooltip: ApexTooltip;
  colors: any;
  grid: any;
  markers: any;
  legend: any;
  responsive: any;
};

export interface PurchaseValue {
  month: string;
  value: number;
}

export interface DepartmentContribution {
  month: string;
  value: number;
}
