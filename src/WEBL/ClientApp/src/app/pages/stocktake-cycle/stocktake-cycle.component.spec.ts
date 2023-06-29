import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { StocktakeCycleComponent } from './stocktake-cycle.component';

describe('StocktakeComponent', () => {
  let component: StocktakeCycleComponent;
  let fixture: ComponentFixture<StocktakeCycleComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [StocktakeCycleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StocktakeCycleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
