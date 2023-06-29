import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { ServiceItemsReportComponent } from './service-items-report.component';

describe('ServiceItemsReportComponent', () => {
  let component: ServiceItemsReportComponent;
  let fixture: ComponentFixture<ServiceItemsReportComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ServiceItemsReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ServiceItemsReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
