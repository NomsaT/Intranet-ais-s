import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { GLVATReportComponent } from './gl-vat-report.component';

describe('GLVATReportComponent', () => {
  let component: GLVATReportComponent;
  let fixture: ComponentFixture<GLVATReportComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [GLVATReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GLVATReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
