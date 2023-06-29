import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { BarcodeListComponent } from './barcode-list.component';

describe('BarcodeListComponent', () => {
  let component: BarcodeListComponent;
  let fixture: ComponentFixture<BarcodeListComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [BarcodeListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BarcodeListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
