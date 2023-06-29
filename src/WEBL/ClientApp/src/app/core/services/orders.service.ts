import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { internalOrderAction } from '../models/internalOrderAction.models';
import { toBeInvoicedItems } from '../models/toBeInvoicedItems.models';

@Injectable({ providedIn: 'root' })
export class OrdersService {
  private baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  editApproveOrder(internalOrderAction: internalOrderAction) {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(this.baseUrl + 'api/InternalOrder/editApproveOrder', internalOrderAction,{ headers: reqHeader });
  }

  editDenyOrder(internalOrderAction: internalOrderAction) {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(this.baseUrl + 'api/InternalOrder/editDenyOrder', internalOrderAction, {headers: reqHeader });
  }

  editReviewOrder(internalOrderAction: internalOrderAction) {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(this.baseUrl + 'api/InternalOrder/editReviewOrder', internalOrderAction, {headers: reqHeader });
  }

  GetInvoiceInternalOrderData(ToBeInvoicedItems: toBeInvoicedItems) {    
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(this.baseUrl + 'api/Invoice/GetInvoiceInternalOrderData', ToBeInvoicedItems, { headers: reqHeader });
  }

  AddUpdateInvoice(invoice) {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(this.baseUrl + 'api/Invoice/AddUpdateInvoice', invoice, { headers: reqHeader });
  }

  RemoveInvoice(id) {
    let params = new HttpParams();

    params = params.append('id', id);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Invoice/RemoveInvoice', { params: params, headers: reqHeader });
  }

  GetInternalOrderData(id,action) {
    let params = new HttpParams();

    params = params.append('id', id);
    params = params.append('action', action);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/InternalOrder/GetInternalOrderData', { params: params, headers: reqHeader });
  }

  AddUpdateInternalOrder(internalOrder) {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(this.baseUrl + 'api/InternalOrder/AddUpdateInternalOrder', internalOrder, { headers: reqHeader });
  }

  AddUpdateDraftInternalOrder(internalOrder) {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(this.baseUrl + 'api/InternalOrder/AddUpdateDraftInternalOrder', internalOrder, { headers: reqHeader });
  }

  AddUpdateDeleteDraftInternalOrder(Id) {
    let params = new HttpParams();

    params = params.append('id', Id);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/InternalOrder/deleteDraftOrder', { params: params, headers: reqHeader });
  }

  getDefaultDepartment() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Department/getDefaultDepartment', { headers: reqHeader });
  }

  /*GRN*/
  GetGrnInternalOrderData(id, action) {
    let params = new HttpParams();

    params = params.append('id', id);
    params = params.append('action', action);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Grn/GetGrnInternalOrderData', { params: params, headers: reqHeader });
  }

  AddUpdateGrn(invoice) {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(this.baseUrl + 'api/Grn/AddUpdateGrn', invoice, { headers: reqHeader });
  }

  RemoveGrn(id) {
    let params = new HttpParams();

    params = params.append('id', id);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Grn/RemoveGrn', { params: params, headers: reqHeader });
  }

  getMonitoredPendingApproval(id) {
    let params = new HttpParams();

    params = params.append('id', id);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/InternalOrder/GetMonitoredItems', { params: params, headers: reqHeader });
  }

  GetLatestGrn(id) {
    let params = new HttpParams();

    params = params.append('id', id);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Grn/GetLatestGrn', { params: params, headers: reqHeader });
  }

  getCapturedGrnItems(id) {
    let params = new HttpParams();

    params = params.append('id', id);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Invoice/GetCapturedGrnItems', { params: params, headers: reqHeader });
  }

  GetInvoiceableItems(internalOrderId) {
    let params = new HttpParams();

    params = params.append('internalOrderId', internalOrderId);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Invoice/GetInvoiceableItems', { params: params, headers: reqHeader });
  }

  GetPDF(Id) {
    let params = new HttpParams();

    params = params.append('id', Id);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/GetPDF', { params: params, headers: reqHeader });
  }

  GetDefaultCompany() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/InternalOrder/getDefaultCompany', { headers: reqHeader });
  }

  CloseProject(Id) {
    let params = new HttpParams();

    params = params.append('id', Id);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Project/CloseProject', { params: params, headers: reqHeader });
  }
}
