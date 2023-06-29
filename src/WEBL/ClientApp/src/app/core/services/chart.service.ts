import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { observableToBeFn } from 'rxjs/internal/testing/TestScheduler';
import { internalOrder } from '../models/internalOrder.models';
import { departmentStock } from '../models/departmentStock.models';


@Injectable({ providedIn: 'root' })
export class ChartService {
  private baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  getOrders(): Observable<internalOrder[]> {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get<internalOrder[]>(this.baseUrl + 'api/Chart/Orders', { headers: reqHeader });
  }

  getDepartmentStock(): Observable<departmentStock[]> {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get<departmentStock[]>(this.baseUrl + 'api/Chart/DepartmentStock', { headers: reqHeader });
  }
}
