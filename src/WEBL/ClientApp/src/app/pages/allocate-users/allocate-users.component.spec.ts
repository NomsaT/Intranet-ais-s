import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { AllocateUsersComponent } from './allocate-users.component';

describe('AllocateUsersComponent', () => {
  let component: AllocateUsersComponent;
  let fixture: ComponentFixture<AllocateUsersComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [AllocateUsersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AllocateUsersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
