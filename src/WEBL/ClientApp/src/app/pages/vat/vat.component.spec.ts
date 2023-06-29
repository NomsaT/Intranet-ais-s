import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { VatComponent } from './vat.component';

describe('VatComponent', () => {
  let component: VatComponent;
  let fixture: ComponentFixture<VatComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [VatComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
