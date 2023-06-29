import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { GRNModifyComponent } from './grn-modify.component';

describe('GRNModifyComponent', () => {
  let component: GRNModifyComponent;
  let fixture: ComponentFixture<GRNModifyComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [GRNModifyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GRNModifyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
