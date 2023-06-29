import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { BarcodeVerificationReportComponent } from './barcode-verification-report.component';

describe('BarcodeVerificationReportComponent', () => {
  let component: BarcodeVerificationReportComponent;
  let fixture: ComponentFixture<BarcodeVerificationReportComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [BarcodeVerificationReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BarcodeVerificationReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
