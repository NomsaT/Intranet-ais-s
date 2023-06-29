import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class QuotationService {
  private baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  GetQuotationsData(id,action) {
    let params = new HttpParams();

    params = params.append('id', id);
    params = params.append('action', action);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Quotation/GetQuotationsData', { params: params, headers: reqHeader });
  }

  AddUpdateQuotations(quote) {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(this.baseUrl + 'api/Quotation/AddUpdateQuotations', quote, { headers: reqHeader });
  }

  AddUpdateDraftQuotations(quote) {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(this.baseUrl + 'api/Quotation/AddUpdateDraftQuotations', quote, { headers: reqHeader });
  }
  
}
