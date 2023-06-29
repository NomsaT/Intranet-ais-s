import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { DepartmentStockComponent } from './department-stock.component';

describe('DepartmentStockComponent', () => {
  let component: DepartmentStockComponent;
  let fixture: ComponentFixture<DepartmentStockComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [DepartmentStockComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DepartmentStockComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
