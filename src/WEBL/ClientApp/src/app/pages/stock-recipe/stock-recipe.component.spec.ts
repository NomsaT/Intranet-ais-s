import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { StockRecipesComponent } from './stock-recipe.component';

describe('StockRecipesComponent', () => {
  let component: StockRecipesComponent;
  let fixture: ComponentFixture<StockRecipesComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [StockRecipesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StockRecipesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
