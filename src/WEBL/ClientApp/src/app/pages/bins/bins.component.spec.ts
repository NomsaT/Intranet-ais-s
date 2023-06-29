import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { BinsComponent } from './bins.component';

describe('BinsComponent', () => {
  let component: BinsComponent;
  let fixture: ComponentFixture<BinsComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [BinsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BinsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
