import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { QuotationModifyComponent } from './quotation-modify.component';

describe('QuotationModifyComponent', () => {
  let component: QuotationModifyComponent;
  let fixture: ComponentFixture<QuotationModifyComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [QuotationModifyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuotationModifyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
