import { Injectable } from '@angular/core';

class Complaints {
  complaint: string;
  count: number;
}

export class ComplaintsWithPercent {
  complaint: string;
  count: number;
  cumulativePercent: number;
}

const complaintsData: Complaints[] = [
  { complaint: 'Feb', count: 780 },
  { complaint: 'May', count: 120 },
  { complaint: 'July', count: 52 },
  { complaint: 'Jan', count: 1123 },
  { complaint: 'March', count: 321 },
  { complaint: 'June', count: 89 },
  { complaint: 'April', count: 222 },
];

@Injectable()
export class Service {
  getComplaintsData(): ComplaintsWithPercent[] {
    const data = complaintsData.sort((a, b) => b.count - a.count);
    const totalCount = data.reduce((prevValue, item) => prevValue + item.count, 0);
    let cumulativeCount = 0;
    return data.map((item, index) => {
      cumulativeCount += item.count;
      return {
        complaint: item.complaint,
        count: item.count,
        cumulativePercent: Math.round(cumulativeCount * 100 / totalCount),
      };
    });
  }
}
