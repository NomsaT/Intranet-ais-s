import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { StockCorrectionComponent } from './stock-correction.component';

describe('StockCorrectionComponent', () => {
  let component: StockCorrectionComponent;
  let fixture: ComponentFixture<StockCorrectionComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [StockCorrectionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StockCorrectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
