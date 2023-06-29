import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { DepartmentUsersComponent } from './department-users.component';

describe('DepartmentUsersComponent', () => {
  let component: DepartmentUsersComponent;
  let fixture: ComponentFixture<DepartmentUsersComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [DepartmentUsersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DepartmentUsersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
