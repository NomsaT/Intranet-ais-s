import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { DepartmentManagerComponent } from './department-managers.component';

describe('DepartmentManagerComponent', () => {
  let component: DepartmentManagerComponent;
  let fixture: ComponentFixture<DepartmentManagerComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [DepartmentManagerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DepartmentManagerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
