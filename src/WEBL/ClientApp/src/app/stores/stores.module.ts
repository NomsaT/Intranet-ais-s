import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FullCalendarModule } from '@fullcalendar/angular';
import bootstrapPlugin from "@fullcalendar/bootstrap";
import dayGridPlugin from '@fullcalendar/daygrid'; // a plugin
import interactionPlugin from '@fullcalendar/interaction'; // a plugin
import { NgbCollapseModule, NgbDropdownModule, NgbModalModule, NgbNavModule, NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';
import { DxAutocompleteModule, DxButtonModule, DxCheckBoxModule, DxColorBoxModule, DxDataGridModule, DxDropDownBoxModule, DxFileManagerModule, DxFileUploaderModule, DxFormModule, DxListModule, DxLoadPanelModule, DxLookupModule, DxPopupModule, DxRadioGroupModule, DxScrollViewModule, DxSelectBoxModule, DxSpeedDialActionModule, DxTabPanelModule, DxTagBoxModule, DxTemplateModule, DxTextAreaModule, DxTooltipModule, DxTreeListModule, DxValidatorModule } from 'devextreme-angular';
import { NgApexchartsModule } from 'ng-apexcharts';
import { LightboxModule } from 'ngx-lightbox';
import { SimplebarAngularModule } from 'simplebar-angular';
import { DashboardService } from '../core/services/dashboard.service';
import { UIModule } from '../shared/ui/ui.module';
import { WidgetModule } from '../shared/widget/widget.module';
import { BarcodeListComponent } from './barcode-list/barcode-list.component';
import { ReprintBarcodeListComponent } from './barcode-list-reprint/barcode-list-reprint.component';
import { BarcodeVerificationReportComponent } from './barcode-verification-report/barcode-verification-report.component';
import { DashboardsStoresModule } from './dashboards/dashboards-stores.module';
import { StoresRoutingModule } from './stores-routing.module';
import { UnitOfMeasurementComponent } from './unit-of-measurement/unit-of-measurement.component';
import { BinOverviewComponent } from './bin-overview/bin-overview.component';

FullCalendarModule.registerPlugins([ // register FullCalendar plugins
  dayGridPlugin,
  interactionPlugin,
  bootstrapPlugin
]);

@NgModule({
  declarations: [
    UnitOfMeasurementComponent,
    BarcodeListComponent,
    ReprintBarcodeListComponent,
    BarcodeVerificationReportComponent,
    BinOverviewComponent
 ],
  imports: [
    CommonModule,
    FormsModule,
    NgbDropdownModule,
    NgbModalModule,
    StoresRoutingModule,
    NgApexchartsModule,
    ReactiveFormsModule,
    DashboardsStoresModule,
    HttpClientModule,
    UIModule,
    WidgetModule,
    FullCalendarModule,
    NgbNavModule,
    NgbTooltipModule,
    NgbCollapseModule,
    SimplebarAngularModule,
    LightboxModule,
    DxDataGridModule,
    DxFileUploaderModule,
    DxLookupModule,
    DxDropDownBoxModule,
    DxTagBoxModule,
    DxTooltipModule,
    DxButtonModule,
    DxPopupModule,
    DxScrollViewModule,
    DxCheckBoxModule,
    DxTextAreaModule,
    DxTabPanelModule,
    DxFormModule,
    DxSpeedDialActionModule,
    DxValidatorModule,
    DxPopupModule,
    DxButtonModule,
    DxTemplateModule,
    DxListModule,
    DxTreeListModule,
    DxSelectBoxModule,
    DxAutocompleteModule,
    DxColorBoxModule,
    DxLoadPanelModule,
    DxRadioGroupModule,
    DxFileManagerModule
  ],
  providers: [
    DashboardService
  ]
})
export class StoresModule { }
