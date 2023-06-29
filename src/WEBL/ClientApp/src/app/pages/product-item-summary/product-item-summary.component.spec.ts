import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { ProductItemSummaryComponent } from './product-item-summary.component';

describe('ProductItemSummaryComponent', () => {
  let component: ProductItemSummaryComponent;
  let fixture: ComponentFixture<ProductItemSummaryComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ProductItemSummaryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductItemSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
