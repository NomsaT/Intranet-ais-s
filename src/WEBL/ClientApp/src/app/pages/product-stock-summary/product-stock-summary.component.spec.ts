import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { ProductStockSummaryComponent } from './product-stock-summary.component';

describe('ProductStockSummaryComponent', () => {
  let component: ProductStockSummaryComponent;
  let fixture: ComponentFixture<ProductStockSummaryComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ProductStockSummaryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductStockSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
