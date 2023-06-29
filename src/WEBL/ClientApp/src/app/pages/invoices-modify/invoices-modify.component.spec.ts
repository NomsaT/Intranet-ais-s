import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { InvoiceModifyComponent } from './invoices-modify.component';

describe('InvoiceModifyComponent', () => {
  let component: InvoiceModifyComponent;
  let fixture: ComponentFixture<InvoiceModifyComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [InvoiceModifyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InvoiceModifyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
