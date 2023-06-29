import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class UserManagementService {
  private baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  getRoles() {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Roles/getRoleFirst', { headers: reqHeader });
  }

  getRolePermissions(id) {
    let params = new HttpParams();

    params = params.append('id', id);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Permissions/getRolePermissions', { params: params,headers: reqHeader });
  }

  getUserPermissions(id) {
    let params = new HttpParams();

    params = params.append('id', id);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/Permissions/getUserPermissions', { params: params, headers: reqHeader });
  }

  assignRolesPermissions(roleId, permissions) {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(this.baseUrl + 'api/Permissions/AssignRolesPermissions', { roleId: roleId, permissions: permissions }, { headers: reqHeader });
  }

  assignUserPermissions(userId, permissions) {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(this.baseUrl + 'api/Permissions/AssignUserPermissions', { userId: userId, permissions: permissions }, { headers: reqHeader });
  }
}
