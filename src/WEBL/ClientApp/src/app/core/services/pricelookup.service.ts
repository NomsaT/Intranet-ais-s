import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { priceIncrease } from '../models/priceIncrease.models';

@Injectable({ providedIn: 'root' })
export class PriceLookUpService {
  private baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }


  putPriceLookUp(increase: priceIncrease) {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(this.baseUrl + 'api/PriceLookUp/ManualUpdate', increase, { headers: reqHeader });
  }
}
