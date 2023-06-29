import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { EmployeePositionComponent } from './employee-position.component';

describe('EmployeePositionComponent', () => {
  let component: EmployeePositionComponent;
  let fixture: ComponentFixture<EmployeePositionComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [EmployeePositionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeePositionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
