import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { PriceLookUpComponent } from './price-lookup.component';

describe('PriceLookUpComponent', () => {
  let component: PriceLookUpComponent;
  let fixture: ComponentFixture<PriceLookUpComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [PriceLookUpComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PriceLookUpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
