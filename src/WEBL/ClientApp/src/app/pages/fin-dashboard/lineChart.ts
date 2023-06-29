import { ChartType } from "./fin-dashboard.model";

export const LineChart: ChartType = {
  chart: {
    height: 350,
    type: "line",
    zoom: {
      enabled: false,
    },
    toolbar: {
      show: false,
    },
  },
  colors: ["#556ee6", "#34c38f"],
  stroke: {
    width: [3, 3],
    curve: "smooth",
  },
  series: [
    {
      name: "Total Department Contribution Value",
      type: "column",
      data: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
    },
    {
      name: "Total Purchase Value",
      type: "line",
      data: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
    }
  ],
  title: {
    text: "",
    align: "left",
  },
  grid: {
    row: {
      colors: ["#f3f3f3", "#f3f3f3"],
      opacity: 0.5,
    },
    borderColor: "#f1f1f1",
  },
  tooltip: {
    y: {
      formatter: (val) => {
        return "R" + val + "";
      },
    },
    x: {
      formatter: (val) => {
        return monthNames[val - 1];
      },
    },
  },
  markers: {
    style: "inverted",
    size: 6,
  },
  xaxis: {
    categories: [
      "Jan",
      "Feb",
      "Mar",
      "Apr",
      "May",
      "Jun",
      "Jul",
      "Aug",
      "Sep",
      "Oct",
      "Nov",
      "Dec",
    ],
    title: {
      text: "Months",
    },
  },
  yaxis: {
    title: {
      text: "Stock Value (R)",
    },
    min: 0,
    labels: {
      /**
       * Allows users to apply a custom formatter function to yaxis labels.
       *
       * @param { String } value - The generated value of the y-axis tick
       * @param { index } index of the tick / currently executing iteration in yaxis labels array
       */
      formatter: function (val) {
        return "R" + val.toFixed(2);
      },
    },
  },
  dataLabels: {
    enabled: true,
    enabledOnSeries: [1]
  },
  legend: {
    position: "top",
    horizontalAlign: "right",
    floating: true,
    offsetY: -15,
    offsetX: -5,
  },
  responsive: [
    {
      breakpoint: 600,
      options: {
        chart: {
          toolbar: {
            show: false,
          },
        },
        legend: {
          show: false,
        },
      },
    },
  ],
  labels: [],
  fill: undefined
};

export const PieChart: ChartType = {
  series: [],
  chart: undefined,
  xaxis: undefined,
  yaxis: undefined,
  title: undefined,
  labels: [],
  stroke: undefined,
  dataLabels: undefined,
  fill: undefined,
  tooltip: undefined,
  colors: undefined,
  grid: undefined,
  markers: undefined,
  legend: undefined,
  responsive: undefined
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
