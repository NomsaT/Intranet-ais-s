import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { LocationStockComponent } from './location-stock.component';

describe('LocationStockComponent', () => {
  let component: LocationStockComponent;
  let fixture: ComponentFixture<LocationStockComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [LocationStockComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LocationStockComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
