import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { GLCodeComponent } from './gl-code.component';

describe('GLCodeComponent', () => {
  let component: GLCodeComponent;
  let fixture: ComponentFixture<GLCodeComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [GLCodeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GLCodeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
