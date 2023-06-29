import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class BarcodesService {
  private baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  PrintingBarcode(barcode) {
    let params = new HttpParams();

    params = params.append('barcode', barcode);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/PrintBarcode/markPrinted', { params: params, headers: reqHeader });
  }

  Printing(data) {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(this.baseUrl + 'api/PrintBarcode/Printing', data, {headers: reqHeader });
  }

  PrintingBinBarcode(data) {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(this.baseUrl + 'api/PrintBarcode/PrintingBinBarcode', data, { headers: reqHeader });
  }

  getTodaysDeliveries() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/DashboardStores/getTodaysDeliveries', { headers: reqHeader });
  }

  getUpcommingDeliveries() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/DashboardStores/getUpcommingDeliveries', { headers: reqHeader });
  }

  getLateDeliveries() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/DashboardStores/getLateDeliveries', { headers: reqHeader });
  }

  DeliveryReminderEmailing(id, type) {
    let params = new HttpParams();

    params = params.append('id', id);
    params = params.append('type', type);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/DashboardStores/DeliveryReminderEmailing', { params: params, headers: reqHeader });
  }

 /* PrintingProductBarcode(barcode) {
    let params = new HttpParams();

    params = params.append('barcode', barcode);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/PrintBarcode/markPrintedProduct', { params: params, headers: reqHeader });
  }*/

  PrintingProduct(data) {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(this.baseUrl + 'api/PrintBarcode/PrintingProduct', data, { headers: reqHeader });
  }

  PrintBarcodePDF() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/PrintBarcode/PrintBarcodePDF', { headers: reqHeader });
  }
}
