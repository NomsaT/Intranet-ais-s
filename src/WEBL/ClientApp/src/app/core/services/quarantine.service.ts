import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class QuarantineService {
  private baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  ReturnToSupplier(data) {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(this.baseUrl + 'api/Quarantine/ReturnToSupplier', data, {headers: reqHeader });
  }

  LogReturnToSupplier(data) {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(this.baseUrl + 'api/Quarantine/LogReturnToSupplier', data, { headers: reqHeader });
  }
  /*getTodaysDeliveries() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/DashboardStores/getTodaysDeliveries', { headers: reqHeader });
  }*/
}
