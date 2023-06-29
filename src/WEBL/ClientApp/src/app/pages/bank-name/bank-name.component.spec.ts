import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { BankNameComponent } from './bank-name.component';

describe('BankNameComponent', () => {
  let component: BankNameComponent;
  let fixture: ComponentFixture<BankNameComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [BankNameComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BankNameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
