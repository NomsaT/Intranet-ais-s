import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { ProductionStockComponent } from './production-stock.component';

describe('ProductionStockComponent', () => {
  let component: ProductionStockComponent;
  let fixture: ComponentFixture<ProductionStockComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ProductionStockComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductionStockComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
