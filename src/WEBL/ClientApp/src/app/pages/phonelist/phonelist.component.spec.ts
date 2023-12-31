import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { PhonelistComponent } from './phonelist.component';

describe('PhonelistComponent', () => {
  let component: PhonelistComponent;
  let fixture: ComponentFixture<PhonelistComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [PhonelistComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PhonelistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
