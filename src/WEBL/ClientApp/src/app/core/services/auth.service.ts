import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Idle } from '@ng-idle/core';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../models/auth.models';

@Injectable({ providedIn: 'root' })

export class AuthenticationService {
  private baseUrl: string;
  public currentUserSubject: BehaviorSubject<User>;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private idle: Idle, private router: Router,) {
    this.baseUrl = baseUrl;
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));

    idle.onTimeout.subscribe(() => {
      //this.logout()
    });
  }

    /**
     * Returns the current user
     */
    public currentUser(): User {
      return this.currentUserSubject.value;
  }

  public HavePermission(permission: number) {
    if (this.currentUser().permissions.indexOf(permission) > -1) {
      return true
    }
    return false;
  }

  public HavePermissions(permissions: number[]) {
    for (let i = 0; i < permissions.length; i++) {
      if (this.currentUser().permissions.indexOf(permissions[i]) > -1) {
        return true
      }
    }
    return false;
  }

  login(account: string, password: string, rememberMe: boolean) {
    return this.http.post<any>(this.baseUrl + 'api/auth/Login', { account, password })
      .pipe(map(user => {
        // login successful if there's a jwt token in the response
        if (user && user.token) {
          // store user details and jwt token in local storage to keep user logged in between page refreshes
          if (rememberMe) {
            localStorage.setItem('currentUser', JSON.stringify(user));
          }
          //TODO: set time per user for idling this.idle.setTimeout(x)
          this.idle.watch();
          this.currentUserSubject.next(user);
          this.SendEmail(user.id);        
        }
        return user;
      }));
  }

  SendEmail(id) {
    let params = new HttpParams();

    params = params.append('id', id);
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.get(this.baseUrl + 'api/users/SendEmail', { params: params, headers: reqHeader }).subscribe(data => {    
      if (data != "") {
        console.log("yes");
      } else {
        console.log("no");
      }
    })
  }

  ResetPassword(account: string) {
    return this.http.post<any>(this.baseUrl + 'api/auth/ResetPassword', { account })
  }

  BlockUser(account: string) {
    return this.http.post<any>(this.baseUrl + 'api/auth/BlockUser', { account })
  }

  SetPassword(account: string, password: string, reference: string) {
    return this.http.post<any>(this.baseUrl + 'api/auth/SetPassword', { account, password, reference })
  }

  AdminSetPassword(id: number, password: string) {
    return this.http.post<any>(this.baseUrl + 'api/auth/AdminSetPassword', { id, password })
  }

  UserSetPassword(password: string) {
    let id = this.currentUser().id;
    return this.http.post<any>(this.baseUrl + 'api/auth/UserSetPassword', { id, password })
  }

    logout() {
      // remove user from local storage to log user out
      localStorage.removeItem('currentUser');
      this.currentUserSubject.next(null);
      this.router.navigate(['/account/login']);
  }

  SetSite(site: string) {
    let user = this.currentUser();
    if (user.site != site) {
    user.site = site;
    this.currentUserSubject.next(user);
    }

  }
}

