import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { priceIncrease } from '../models/priceIncrease.models';

@Injectable({ providedIn: 'root' })
export class PriceIncreaseService {
  private baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  postPriceIncrease(increase: priceIncrease) {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json'});
    return this.http.post<any>(this.baseUrl + 'api/PriceIncrease/PriceIncrease', increase, { headers: reqHeader });
  }

  putPriceIncrease(increase: priceIncrease) {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.put<any>(this.baseUrl + 'api/PriceIncrease/PriceIncreaseEdit', increase, { headers: reqHeader });
  }

  getItemInfo(id) {
    let params = new HttpParams();

    params = params.append('id', id);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/PriceIncrease/getItemInfo', { params: params, headers: reqHeader });
  }

  removeItemInfo(id) {
    let params = new HttpParams();

    params = params.append('id', id);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.delete(this.baseUrl + 'api/PriceIncrease/removeItemInfo', { params: params, headers: reqHeader });
  }

}
