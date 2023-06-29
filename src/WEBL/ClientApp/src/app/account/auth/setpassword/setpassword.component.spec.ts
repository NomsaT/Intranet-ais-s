import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { SetpasswordComponent } from './setpassword.component';

describe('PasswordresetComponent', () => {
  let component: SetpasswordComponent;
  let fixture: ComponentFixture<SetpasswordComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [SetpasswordComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SetpasswordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
