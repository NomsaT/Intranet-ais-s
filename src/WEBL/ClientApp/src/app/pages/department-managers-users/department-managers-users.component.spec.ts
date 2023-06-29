import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { DepartmentManagerUsersComponent } from './department-managers-users.component';

describe('DepartmentManagerUsersComponent', () => {
  let component: DepartmentManagerUsersComponent;
  let fixture: ComponentFixture<DepartmentManagerUsersComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [DepartmentManagerUsersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DepartmentManagerUsersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
