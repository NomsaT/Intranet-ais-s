import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { CaptureModifyComponent } from './capture-modify.component';

describe('CaptureModifyComponent', () => {
  let component: CaptureModifyComponent;
  let fixture: ComponentFixture<CaptureModifyComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [CaptureModifyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CaptureModifyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
