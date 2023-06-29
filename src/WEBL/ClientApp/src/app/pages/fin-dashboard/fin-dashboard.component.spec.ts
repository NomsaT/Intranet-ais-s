import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FinDashboardComponent } from './fin-dashboard.component';

describe('FinDashboardComponent', () => {
  let component: FinDashboardComponent;
  let fixture: ComponentFixture<FinDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FinDashboardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FinDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
