import { ChartType } from './chart.model';

export class StackedBarChart implements ChartType {
  chart?: any;
  render?: any;
  noData?: any;
  series?: any;
  categories?: any;
  stroke?: any;
  style?: any;
  labels?: any;
  markers?: any;
  legend?: any;
  xaxis?: any;
  yaxis?: any;
  tooltip?: any;
  grid?: any;
  type?: any;
  stacked?: any;
  height?: any;
  dataLabels?: any;
  title?: any;
  plotOptions?: any;
  responsive?: any;
  fill?: any;
  xAxis?: any = [];

  constructor() {
    this.series = [
      {
        name: "Details",
        data: [0, 0, 0, 0, 0],
        type: "column",
      }
    ],
      this.chart = {
        type: "bar",
        height: 350,
        stacked: true,
      },
      this.responsive = [
        {
          breakpoint: 480,
          options: {
            legend: {
              position: "bottom",
              offsetX: -10,
              offsetY: 0
            }
          }
        }
      ],
      this.plotOptions = {
        bar: {
          horizontal: false,
          columnWidth: "25%",
          /*barHeight: "15%",*/
        }
      },
      this.xaxis = {
        type: "category",
        title: {
          text: "Departments",
        },
        categories: [],
      },
      this.dataLabels = {
        enabled: false,

      }
    this.fill = {
      opacity: 1
    }
    this.yaxis = [{
      title: {
        text: "Thousands (ZAR)"
      },
      labels: {
        formatter: function (val) {
          return "R" + val.toFixed(2);
        },
      }
    }]
  }
}

export interface arrayModel {
  department: string;
  value: number;
}
export class DepartStock {
  constructor(public departmentIndex: number, public value: number) { }

  get name(): string | undefined {
    return departmentNames[this.departmentIndex - 1];
  }
}
export const departmentNames = [
  "General Department",
  "Flat Sheeting",
  "Bubble Ends",
  "Raw Materials",
  "Sales",
  "Information Technology"
];






