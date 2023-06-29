import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { ReturnToSupplierReportComponent } from './return-to-supplier-report.component';

describe('ReturnToSupplierReportComponent', () => {
  let component: ReturnToSupplierReportComponent;
  let fixture: ComponentFixture<ReturnToSupplierReportComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ReturnToSupplierReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReturnToSupplierReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
