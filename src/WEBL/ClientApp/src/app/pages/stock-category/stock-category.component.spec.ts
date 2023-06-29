import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { StockCategoryComponent } from './stock-category.component';

describe('StockCategoryComponent', () => {
  let component: StockCategoryComponent;
  let fixture: ComponentFixture<StockCategoryComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [StockCategoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StockCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
