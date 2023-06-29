import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { CostTypesComponent } from './cost-types.component';

describe('CostTypesComponent', () => {
  let component: CostTypesComponent;
  let fixture: ComponentFixture<CostTypesComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [CostTypesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CostTypesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
