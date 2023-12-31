import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { ProductsSummaryComponent } from './products-summary.component';

describe('ProductsSummaryComponent', () => {
  let component: ProductsSummaryComponent;
  let fixture: ComponentFixture<ProductsSummaryComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ProductsSummaryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductsSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
