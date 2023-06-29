import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { activateUser } from '../../core/models/activateUser.models';
import { User } from '../models/auth.models';

@Injectable({ providedIn: 'root' })
export class UserProfileService {
  private baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  getAll() {
      return this.http.get<User[]>(`/api/login`);
  }

  register(user: User) {
      return this.http.post(`/users/register`, user);
  }

  getAllManagers() {
    return this.http.get<any>(this.baseUrl + 'api/FullEmployeeName');
}

  activeUser(user: activateUser) {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(this.baseUrl + 'api/users/ActivateUser', user, {headers: reqHeader });
  }

  deactiveUser(user: activateUser) {
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(this.baseUrl + 'api/users/DeactivateUser', user, { headers: reqHeader });
  }
}
