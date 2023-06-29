import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { StockRecipeItemComponent } from './stock-recipe-item.component';

describe('StockRecipeItemComponent', () => {
  let component: StockRecipeItemComponent;
  let fixture: ComponentFixture<StockRecipeItemComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [StockRecipeItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StockRecipeItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
