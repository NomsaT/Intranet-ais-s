import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { stockQuantity } from '../models/stockQuantity.models';
import { stockTransferDepartment } from '../models/stockTransferDepartment.models';
import { stockTransferLocation } from '../../core/models/stockTransferLocation.models';

@Injectable({ providedIn: 'root' })
export class StockService {
  private baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  AddStock(stock: stockQuantity) {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(this.baseUrl + 'api/stockManage/AddStock', stock, { headers: reqHeader });
  }

  TransferStockDepartment(stock: stockTransferDepartment) {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(this.baseUrl + 'api/stockManage/TransferStockDepartment', stock, { headers: reqHeader });
  }

  TransferStockLocation(stock: stockTransferLocation) {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(this.baseUrl + 'api/stockManage/TransferStockLocation', stock, { headers: reqHeader });
  }

  RemoveStock(stock: stockQuantity) {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(this.baseUrl + 'api/stockManage/RemoveStock', stock, { headers: reqHeader });
  }

  RemoveConsumeStock(stock: stockQuantity) {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(this.baseUrl + 'api/stockManage/RemoveConsumeStock', stock, { headers: reqHeader });
  }

 /* SetStock(stock: stockQuantity) {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(this.baseUrl + 'api/stockManage/SetStock', stock, { headers: reqHeader });
  }*/

  GetStockPrice(id) {
    let params = new HttpParams();

    params = params.append('id', id);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/stockManage/GetStockPrice', { params: params, headers: reqHeader });
  }

  GetStockUOM(id) {
    let params = new HttpParams();

    params = params.append('id', id);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/stockManage/GetStockUOM', { params: params, headers: reqHeader });
  }

  GetMinThreshold(id) {
    let params = new HttpParams();

    params = params.append('id', id);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/stockManage/GetMinThreshold', { params: params, headers: reqHeader });
  }

  CheckSKU(code,id) {
    let params = new HttpParams();

    params = params.append('code', code);
    params = params.append('id', id);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/stockManage/CheckSKU', { params: params, headers: reqHeader });
  }

  GetMixRecipe(id) {
    let params = new HttpParams();

    params = params.append('id', id);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/stockManage/GetMixRecipe', { params: params, headers: reqHeader });
  }

  MixStock(stock: any) {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(this.baseUrl + 'api/stockManage/MixStock', stock, { headers: reqHeader });
  }

  ImportStock() {
    let params = new HttpParams();
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get<string[]>(this.baseUrl + 'api/Import/Stock', { params: params, headers: reqHeader });
  }

  GetStorageType() {
    let params = new HttpParams();
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/StorageType', { params: params, headers: reqHeader });
  }

  GetDefaultStore(id) {
    let params = new HttpParams();

    params = params.append('id', id);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/plantLocation/getDefaultStore', { params: params, headers: reqHeader });
  }

  GetDefaultLocation(id) {
    let params = new HttpParams();

    params = params.append('id', id);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/plantLocation/filterLocations', { params: params, headers: reqHeader });
  }

  GetUom(id) {
    let params = new HttpParams();

    params = params.append('stockId', id);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/UnitOfMeasurement/getUOMbyStock', { params: params, headers: reqHeader });
  }

  GetStockData(id, action) {
    let params = new HttpParams();

    params = params.append('id', id);
    params = params.append('action', action);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Stock/GetStockData', { params: params, headers: reqHeader });
  }

  AddUpdateStock(stockItem) {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(this.baseUrl + 'api/Stock/AddUpdateStock', stockItem, { headers: reqHeader });
  }

  getTotalStockDep(id,stockId) {
    let params = new HttpParams();

    params = params.append('id', id);
    params = params.append('stockId', stockId);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Stock/GetTotalStockDep', { params: params, headers: reqHeader });
  }

  getTotalStockLocation(id,stockId) {
    let params = new HttpParams();

    params = params.append('id', id);
    params = params.append('stockId', stockId);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Stock/getTotalStockLocation', { params: params, headers: reqHeader });
  }

  getTotalStockStore(locationId,storeId,stockId) {
    let params = new HttpParams();

    params = params.append('locationId', locationId);
    params = params.append('storeId', storeId);
    params = params.append('stockId', stockId);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Stock/getTotalStockStore', { params: params, headers: reqHeader });
  }

  GetCurrency(id) {
    let params = new HttpParams();

    params = params.append('id', id);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/stockManage/GetCurrency', { params: params, headers: reqHeader });
  }

  GetVat() {    
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Vat/getVatPerc', { headers: reqHeader });
  }

  GetProductStock(productId) {
    let params = new HttpParams();

    params = params.append('productId', productId);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/ProductStock/GetTotalProductStock', { params: params, headers: reqHeader });
  }

  GetProductItem(productId) {
    let params = new HttpParams();

    params = params.append('productId', productId);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/ProductItem/GetTotalProductItem', { params: params, headers: reqHeader });
  }

  RemoveAllProductStock(productId) {
    let params = new HttpParams();

    params = params.append('productId', productId);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/ProductStock/RemoveAllProductStock', { params: params, headers: reqHeader });
  }

  RemoveAllProductItem(productId) {
    let params = new HttpParams();

    params = params.append('productId', productId);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/ProductItem/RemoveAllProductItem', { params: params, headers: reqHeader });
  }
}
