import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { OrdersModifyComponent } from './orders-modify.component';

describe('OrdersModifyComponent', () => {
  let component: OrdersModifyComponent;
  let fixture: ComponentFixture<OrdersModifyComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [OrdersModifyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OrdersModifyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
