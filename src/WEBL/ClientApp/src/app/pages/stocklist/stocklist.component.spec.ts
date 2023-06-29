import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { StocklistComponent } from './stocklist.component';

describe('StocklistComponent', () => {
  let component: StocklistComponent;
  let fixture: ComponentFixture<StocklistComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [StocklistComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StocklistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
