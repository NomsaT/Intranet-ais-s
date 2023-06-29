import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { ProjectCostingComponent } from './project-costing.component';

describe('ProjectCostingComponent', () => {
  let component: ProjectCostingComponent;
  let fixture: ComponentFixture<ProjectCostingComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ProjectCostingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectCostingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
