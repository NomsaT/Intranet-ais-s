import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { OnceOffItemsReportComponent } from './once-off-items-report.component';

describe('OnceOffItemsReportComponent', () => {
  let component: OnceOffItemsReportComponent;
  let fixture: ComponentFixture<OnceOffItemsReportComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [OnceOffItemsReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OnceOffItemsReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
