import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { StocktakeComponent } from './stocktake.component';

describe('StocktakeComponent', () => {
  let component: StocktakeComponent;
  let fixture: ComponentFixture<StocktakeComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [StocktakeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StocktakeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
