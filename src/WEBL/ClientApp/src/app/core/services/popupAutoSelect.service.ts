import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class popupAutoSelectService {
  private baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  GetNewestSupplier() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/autoSelect/GetNewestSupplier', {headers: reqHeader });
  }

  GetNewestStockCategory() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/autoSelect/GetNewestStockCategory', { headers: reqHeader });
  }

  GetNewestStockGroup() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/autoSelect/GetNewestStockGroup', { headers: reqHeader });
  }

  GetNewestDepartment() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/autoSelect/GetNewestDepartment', { headers: reqHeader });
  }

  GetNewestLocation() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/autoSelect/GetNewestLocation', { headers: reqHeader });
  }

  GetNewestUom() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/autoSelect/GetNewestUom', { headers: reqHeader });
  }

  GetNewestPaymentMethod() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/autoSelect/GetNewestPaymentMethod', { headers: reqHeader });
  }

  GetNewestBankName() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/autoSelect/GetNewestBankName', { headers: reqHeader });
  }

  GetNewestCostType() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/autoSelect/GetNewestCostType', { headers: reqHeader });
  }

  GetNewestStorageType() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/autoSelect/GetNewestStorageType', { headers: reqHeader });
  }
}
