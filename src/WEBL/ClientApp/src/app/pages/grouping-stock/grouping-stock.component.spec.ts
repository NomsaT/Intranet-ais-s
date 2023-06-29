import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { GroupingStockComponent } from './grouping-stock.component';

describe('GroupingStockComponent', () => {
  let component: GroupingStockComponent;
  let fixture: ComponentFixture<GroupingStockComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [GroupingStockComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GroupingStockComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
