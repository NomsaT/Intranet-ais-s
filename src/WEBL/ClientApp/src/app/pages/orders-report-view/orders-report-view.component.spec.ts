import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { OrdersReportViewComponent } from './orders-report-view.component';

describe('OrdersReportViewComponent', () => {
  let component: OrdersReportViewComponent;
  let fixture: ComponentFixture<OrdersReportViewComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [OrdersReportViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OrdersReportViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
