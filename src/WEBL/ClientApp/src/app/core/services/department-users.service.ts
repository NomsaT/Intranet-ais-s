import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class DepartmentUserService {
  private baseUrl: string;
  public departmentAllocation: BehaviorSubject<number>;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
    this.departmentAllocation = new BehaviorSubject<number>(0);
  }

}
