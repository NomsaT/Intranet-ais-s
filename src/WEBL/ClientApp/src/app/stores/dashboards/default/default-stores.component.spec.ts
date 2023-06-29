import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { DefaultStoresComponent } from './default-stores.component';

describe('DefaultStoresComponent', () => {
  let component: DefaultStoresComponent;
  let fixture: ComponentFixture<DefaultStoresComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [DefaultStoresComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DefaultStoresComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
