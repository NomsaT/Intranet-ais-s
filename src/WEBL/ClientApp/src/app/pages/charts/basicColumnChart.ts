import { ChartType } from './chart.model';

export class BasicColumnChart implements ChartType {
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
  month?: any = [];

  constructor() {
    this.series = [
      {
        name: "Details",
        data: [0, 0, 0, 0],

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
            chart: {
              toolbar: {
                show: false,
              },
              legend: {
                show: false,
              },
              dataLabels: {
                enabled: false
              },
            },
          },
        }
      ],
      this.plotOptions = {
        bar: {
          horizontal: false,
          columnWidth: "25%",
          dataLabels: {
            formatter: function (val) {
              return val.toFixed(2);
            },
            offsetX: 30,
            enabled: false
          },
        }
      },
      this.xaxis = {
        type: "category",
        title: {
          text: "Quarter",
        },
        categories: ["Q1", "Q2", "Q3", "Q4"]
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

export interface arrModel {
  month: string | number;
  value: number;
}

export class Quarter {
  months: Month[] = [];

  constructor(
    public index: number,
    public minMonth: number,
    public maxMonth: number
  ) { }

  get value() {
    return this.months.map((v) => v.value).reduce((a, b) => a + b, 0);
  }
}

export class Month {
  constructor(public monthIndex: number, public value: number) { }

  get name(): string | undefined {
    return monthNames[this.monthIndex - 1];
  }
}

export const monthNames = [
  "January",
  "February",
  "March",
  "April",
  "May",
  "June",
  "July",
  "August",
  "September",
  "October",
  "November",
  "December",
];
