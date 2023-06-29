import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { AssignRolesComponent } from './assign-roles.component';

describe('AssignRolesComponent', () => {
  let component: AssignRolesComponent;
  let fixture: ComponentFixture<AssignRolesComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [AssignRolesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AssignRolesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
