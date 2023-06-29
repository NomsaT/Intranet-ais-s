import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BinOverviewComponent } from './bin-overview.component';

describe('BinOverviewComponent', () => {
  let component: BinOverviewComponent;
  let fixture: ComponentFixture<BinOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BinOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BinOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
