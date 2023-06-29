import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbDropdownModule, NgbNavModule, NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';
import { NgApexchartsModule } from 'ng-apexcharts';
import { SimplebarAngularModule } from 'simplebar-angular';
import { UIModule } from '../../shared/ui/ui.module';
import { WidgetModule } from '../../shared/widget/widget.module';
import { DashboardsRoutingModule } from './dashboards-routing.module';
import { DefaultComponent } from './default/default.component';
import { DxCheckBoxModule, DxScrollViewModule, DxPopupModule, DxButtonModule, DxFormModule, DxLoadPanelModule } from 'devextreme-angular';




@NgModule({
  declarations: [DefaultComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    DashboardsRoutingModule,
    UIModule,
    NgbDropdownModule,
    NgbTooltipModule,
    NgbNavModule,
    WidgetModule,
    NgApexchartsModule,
    SimplebarAngularModule,
    DxCheckBoxModule,
    DxScrollViewModule,
    DxPopupModule,
    DxButtonModule,
    DxFormModule,
    DxLoadPanelModule
  ]
})
export class DashboardsModule { }
