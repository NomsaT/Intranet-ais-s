import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { OrdersReportComponent } from './orders-report.component';

describe('OrdersReportComponent', () => {
  let component: OrdersReportComponent;
  let fixture: ComponentFixture<OrdersReportComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [OrdersReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OrdersReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
