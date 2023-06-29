import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { UnitOfMeasurementComponent } from './unit-of-measurement.component';

describe('UnitOfMeasurementComponent', () => {
  let component: UnitOfMeasurementComponent;
  let fixture: ComponentFixture<UnitOfMeasurementComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [UnitOfMeasurementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UnitOfMeasurementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
