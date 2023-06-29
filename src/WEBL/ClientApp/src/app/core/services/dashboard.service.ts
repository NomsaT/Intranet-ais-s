import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DepartmentContribution, PurchaseValue } from 'src/app/pages/fin-dashboard/fin-dashboard.model';

@Injectable({ providedIn: 'root' })
export class DashboardService {
  private baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  getBirthdays() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Dashboard/Birthdays', { headers: reqHeader });
  }

  getCurrencyUSD() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Dashboard/CurrencyUSD', { headers: reqHeader });
  }

  getCurrencyEUR() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Dashboard/CurrencyEUR', { headers: reqHeader });
  }

  getPrevCurrencyUSD() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Dashboard/PrevCurrencyUSD', { headers: reqHeader });
  }

  getPrevCurrencyEUR() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Dashboard/PrevCurrencyEUR', { headers: reqHeader });
  }

  getFlaggedItems() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Dashboard/FlaggedItems', { headers: reqHeader });
  }

  getPriceLookupBadge() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Dashboard/PriceLookUpBadge', { headers: reqHeader });
  }

  getPrintingBadge() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Dashboard/PrintingBadge', { headers: reqHeader });
  }

  getWeeklyStockItems() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Dashboard/StockItems', { headers: reqHeader });
  }

  getTotalStockItems() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Dashboard/TotalStockItems', { headers: reqHeader });
  }

  getMonthlyStockValue() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Dashboard/MonthlyStockValue', { headers: reqHeader });
  }

  getMonthlyStockPercentage() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Dashboard/GetMonthlyStockPercentage', { headers: reqHeader });
  }

  getProductuctionStoreValue() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Dashboard/ProductuctionStoreValue', { headers: reqHeader });
  }

  getDepartments() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Dashboard/TotalDepartments', { headers: reqHeader });
  }

  getLocations() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Dashboard/TotalLocations', { headers: reqHeader });
  }

  getMinStockThreshold() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Dashboard/MinStockThreshold', { headers: reqHeader });
  }

  getDepartmentStock() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get<any[]>(this.baseUrl + 'api/Dashboard/DepartmentStock', { headers: reqHeader });
  }

  getProductionStock() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get<any[]>(this.baseUrl + 'api/Dashboard/GetProductionStock', { headers: reqHeader });
  }

  getProductionStockItems() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get<any[]>(this.baseUrl + 'api/ProductionStock/GetProductionStockItems', { headers: reqHeader });
  }


  getOrders() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get<any>(this.baseUrl + 'api/Dashboard/Orders', { headers: reqHeader });
  }

  getAllOrders() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get<any>(this.baseUrl + 'api/InternalOrder/GetAllInternalOrders', { headers: reqHeader });
  }


  getFilteredOrders(userId) {
    let params = new HttpParams();

    params = params.append('userId', userId);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get<any>(this.baseUrl + 'api/Dashboard/FilteredOrders', { params: params, headers: reqHeader });
  }

  getOrdersReview() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Dashboard/OrdersReview', { headers: reqHeader });
  }

  getFilteredOrdersReview(userId) {
    let params = new HttpParams();

    params = params.append('userId', userId);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Dashboard/FilteredOrdersReview', { params: params, headers: reqHeader });
  }

  getDBVersion() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/DBVersion/getDBVersion', { headers: reqHeader });
  }

  getStocktake() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Dashboard/Stocktake', { headers: reqHeader });
  }

  getTotalApprovedOrders() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Dashboard/getTotalApprovedOrders', { headers: reqHeader });
  }

  getTotalPendingOrders() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Dashboard/getTotalPendingOrders', { headers: reqHeader });
  }

  getTotalPendingPriceOrders() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Dashboard/getTotalPendingPriceOrders', { headers: reqHeader });
  }

  getTotalReviewOrders() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Dashboard/getTotalReviewOrders', { headers: reqHeader });
  }

  getTotalDraftOrders() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Dashboard/getTotalDraftOrders', { headers: reqHeader });
  }

  getDonutData(): Observable<number[]> {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get<number[]>(this.baseUrl + 'api/Dashboard/GetDonutData', { headers: reqHeader });
  }

  getPurchaseValue(): Observable<PurchaseValue[]> {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get<PurchaseValue[]>(this.baseUrl + 'api/Dashboard/GetPurchaseValue', { headers: reqHeader });
  }

  getDepartmentContribution(): Observable<DepartmentContribution[]> {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get<DepartmentContribution[]>(this.baseUrl + 'api/Dashboard/GetDepartmentContribution', { headers: reqHeader });
  }
}
