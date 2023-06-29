import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class StocktakeService {
  private baseUrl: string;
  public getCycle: BehaviorSubject<number>;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
    this.getCycle = new BehaviorSubject<number>(0);
  }
  approveStocktake(id) {
    let params = new HttpParams();

    params = params.append('id', id);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/stocktake/ApproveStocktake', { params: params, headers: reqHeader });
  }
  recountStocktake(id, userId: number) {
    let params = new HttpParams();

    params = params.append('id', id);
    params = params.append('userId', userId);

    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/stocktake/RecountStocktake', { params: params, headers: reqHeader });
  }
  getLog(): Observable<any[]>{
    let params = new HttpParams();

    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get<any[]>(this.baseUrl + 'api/stocktake/GetStocktakeLog', { params: params, headers: reqHeader });
  }
  getStocktake() {
    let params = new HttpParams();

    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/stocktake', { params: params, headers: reqHeader });
  }
  getStocktakeReport() {
    let params = new HttpParams();

    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/stocktakeReport', { params: params, headers: reqHeader });
  }
  getStocktakePeriod() {
   let params = new HttpParams();

   const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
   return this.http.get(this.baseUrl + 'api/stocktakePeriod', { params: params, headers: reqHeader });
  }
}
