import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { ReprintBarcodeListComponent } from './barcode-list-reprint.component';

describe('ReprintBarcodeListComponent', () => {
  let component: ReprintBarcodeListComponent;
  let fixture: ComponentFixture<ReprintBarcodeListComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ReprintBarcodeListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReprintBarcodeListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
