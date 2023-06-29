import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbDropdownModule, NgbNavModule, NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';
import { NgApexchartsModule } from 'ng-apexcharts';
import { SimplebarAngularModule } from 'simplebar-angular';
import { UIModule } from '../../shared/ui/ui.module';
import { WidgetModule } from '../../shared/widget/widget.module';
import { DashboardsStoresRoutingModule } from './dashboards-stores-routing.module';
import { DefaultStoresComponent } from './default/default-stores.component';
import { DxCheckBoxModule, DxScrollViewModule, DxPopupModule, DxButtonModule, DxFormModule, DxLoadPanelModule, DxDataGridModule } from 'devextreme-angular';




@NgModule({
  declarations: [DefaultStoresComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    DashboardsStoresRoutingModule,
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
    DxLoadPanelModule,
    DxDataGridModule
  ]
})
export class DashboardsStoresModule { }
