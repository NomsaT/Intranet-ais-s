import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { StockModifyComponent } from './stocks-modify.component';

describe('StockModifyComponent', () => {
  let component: StockModifyComponent;
  let fixture: ComponentFixture<StockModifyComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [StockModifyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StockModifyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
