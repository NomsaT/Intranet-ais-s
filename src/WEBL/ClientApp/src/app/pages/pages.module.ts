import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { FullCalendarModule } from "@fullcalendar/angular";
import bootstrapPlugin from "@fullcalendar/bootstrap";
import dayGridPlugin from '@fullcalendar/daygrid'; // a plugin
import interactionPlugin from '@fullcalendar/interaction'; // a plugin
import { NgbCollapseModule, NgbDropdownModule, NgbModalModule, NgbNavModule, NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';
import { DxAutocompleteModule, DxBoxModule, DxButtonModule, DxLinearGaugeModule, DxCheckBoxModule, DxColorBoxModule, DxDataGridModule, DxDropDownBoxModule, DxFileManagerModule, DxFileUploaderModule, DxFormModule, DxListModule, DxLoadPanelModule, DxLookupModule, DxPopupModule, DxRadioGroupModule, DxScrollViewModule, DxSelectBoxModule, DxSpeedDialActionModule, DxTabPanelModule, DxTagBoxModule, DxTemplateModule, DxTextAreaModule, DxTooltipModule, DxTreeListModule, DxValidatorModule, DxResponsiveBoxModule } from 'devextreme-angular';
import { NgApexchartsModule } from 'ng-apexcharts';
import { LightboxModule } from 'ngx-lightbox';
import { SimplebarAngularModule } from 'simplebar-angular';
import { DashboardService } from '../core/services/dashboard.service';
import { OrdersService } from '../core/services/orders.service';
import { QuotationService } from '../core/services/quotation.service';
import { PriceIncreaseService } from '../core/services/priceIncrease.service';
import { PriceLookUpService } from '../core/services/pricelookup.service';
import { StocktakeService } from '../core/services/stocktake.service';
import { StockService } from '../core/services/stock.service';
import { BarcodesService } from '../core/services/barcodes.service';
import { QuarantineService } from '../core/services/quarantine.service';
import { popupAutoSelectService } from '../core/services/popupAutoSelect.service';
import { SupplierService } from '../core/services/supplier.service';
import { UserProfileService } from '../core/services/user.service';
import { UserManagementService } from '../core/services/usermanagement.service';
import { UIModule } from '../shared/ui/ui.module';
import { WidgetModule } from '../shared/widget/widget.module';
import { AllocateUsersComponent } from './allocate-users/allocate-users.component';
import { AssignRolesComponent } from './assign-roles/assign-roles.component';
import { CustomerComponent } from './customer/customer.component';
import { DashboardsModule } from './dashboards/dashboards.module';
import { DepartmentManagerUsersComponent } from './department-managers-users/department-managers-users.component';
import { DepartmentManagerComponent } from './department-managers/department-managers.component';
import { DepartmentComponent } from './department/department.component';
import { EmployeePositionComponent } from './employee-position/employee-position.component';
import { FileManagementComponent } from './file-management/file-management.component';
import { ImportFilesComponent } from './import-files/import-files.component';
import { InvoiceModifyComponent } from './invoices-modify/invoices-modify.component';
import { GRNModifyComponent } from './grn-modify/grn-modify.component';
import { OrdersModifyComponent } from './orders-modify/orders-modify.component';
import { QuotationModifyComponent } from './quotation-modify/quotation-modify.component';
import { StockModifyComponent } from './stocks-modify/stocks-modify.component';
import { OrdersReportViewComponent } from './orders-report-view/orders-report-view.component';
import { InvoiceComponent } from './invoices/invoices.component';
import { GRNComponent } from './grn/grn.component';
import { JobComponent } from './job/job.component';
import { OrdersReportComponent } from './orders-report/orders-report.component';
import { OrdersComponent } from './orders/orders.component';
import { QuotationComponent } from './quotation/quotation.component';
import { PagesRoutingModule } from './pages-routing.module';
import { PaymentMethodComponent } from './payment-methods/payment-methods.component';
import { PermissionsComponent } from './permissions/permissions.component';
import { PlantLocationComponent } from './plant-location/plant-location.component';
import { StoresComponent } from './stores/stores.component';
import { StoreTypeComponent } from './store-type/store-type.component';
import { GLCodeComponent } from './gl-code/gl-code.component';
import { BankNameComponent } from './bank-name/bank-name.component';
import { ProjectComponent } from './project/project.component';
import { CompanyComponent } from './company/company.component';
import { BinsComponent } from './bins/bins.component';
import { ProjectCostingComponent } from './project-costing/project-costing.component';
import { ReturnToSupplierComponent } from './return-to-supplier/return-to-supplier.component';
import { CostTypesComponent } from './cost-types/cost-types.component';
import { PriceLookUpComponent } from './price-lookup/price-lookup.component';
import { ProductStockComponent } from './product-stock/product-stock.component';
import { ProductStockSummaryComponent } from './product-stock-summary/product-stock-summary.component';
import { ProductItemComponent } from './product-item/product-item.component';
import { ProductItemSummaryComponent } from './product-item-summary/product-item-summary.component';
import { ProductsComponent } from './products/products.component';
import { ProductsSummaryComponent } from './products-summary/products-summary.component';
import { StockRecipesComponent } from './stock-recipe/stock-recipe.component';
import { StockRecipeItemComponent } from './stock-recipe-item/stock-recipe-item.component';
import { DepartmentStockComponent } from './department-stock/department-stock.component';
import { GroupingStockComponent } from './grouping-stock/grouping-stock.component';
import { LocationStockComponent } from './location-stock/location-stock.component';
import { StockTransferComponent } from './stock-transfer/stock-transfer.component';
import { DepartmentUsersComponent } from './department-users/department-users.component';
import { RolesComponent } from './roles/roles.component';
import { StockCategoryComponent } from './stock-category/stock-category.component';
import { StockCorrectionComponent } from './stock-correction/stock-correction.component';
import { StockGroupComponent } from './stock-group/stock-group.component';
import { StockReportComponent } from './stock-report/stock-report.component';
import { ServiceItemsReportComponent } from './service-items-report/service-items-report.component';
import { ReturnToSupplierReportComponent } from './return-to-supplier-report/return-to-supplier-report.component';
import { OnceOffItemsReportComponent } from './once-off-items-report/once-off-items-report.component';
import { GLVATReportComponent } from './gl-vat-report/gl-vat-report.component';
import { ChartsComponent } from './charts/charts.component';
import { StocklistComponent } from './stocklist/stocklist.component';
import { StorageTypeComponent } from './storage-type/storage-type.component';
import { VatComponent } from './vat/vat.component';
import { BirthdayComponent } from './birthday/birthday.component';
import { PhonelistComponent } from './phonelist/phonelist.component';
import { SupplierComponent } from './supplier/supplier.component';
import { UnitOfMeasurementComponent } from './unit-of-measurement/unit-of-measurement.component';
import { UsersComponent } from './users/users.component';
import { ProductionStockComponent } from './production-stock/production-stock.component';
import { ChartService } from '../core/services/chart.service';
import { SafeSvgPipe } from "./products/safe-svg.pipe";
import { DxChartModule } from 'devextreme-angular';
import { StocktakeComponent } from "./stocktake/stocktake.component";
import { FinDashboardComponent } from './fin-dashboard/fin-dashboard.component';
import { ProductionDashboardComponent } from './production-dashboard/production-dashboard.component';
import { StocktakeCycleComponent } from "./stocktake-cycle/stocktake-cycle.component";

FullCalendarModule.registerPlugins([
  // register FullCalendar plugins
  dayGridPlugin,
  interactionPlugin,
  bootstrapPlugin,
]);

@NgModule({
  declarations: [
    SafeSvgPipe,
    CustomerComponent,
    SupplierComponent,
    UnitOfMeasurementComponent,
    PlantLocationComponent,
    StoresComponent,
    StoreTypeComponent,
    GLCodeComponent,
    BankNameComponent,
    ProjectComponent,
    CompanyComponent,
    BinsComponent,
    ProjectCostingComponent,
    ReturnToSupplierComponent,
    CostTypesComponent,
    DepartmentComponent,
    DepartmentManagerComponent,
    UsersComponent,
    RolesComponent,
    ProductsComponent,
    ProductsSummaryComponent,
    StockRecipesComponent,
    StockRecipeItemComponent,
    ProductStockComponent,
    ProductItemComponent,
    ProductStockSummaryComponent,
    ProductItemSummaryComponent,
    DepartmentManagerUsersComponent,
    DepartmentStockComponent,
    LocationStockComponent,
    GroupingStockComponent,
    StockTransferComponent,
    PermissionsComponent,
    AssignRolesComponent,
    PriceLookUpComponent,
    JobComponent,
    StocklistComponent,
    StockCategoryComponent,
    PaymentMethodComponent,
    StockReportComponent,
    ServiceItemsReportComponent,
    ReturnToSupplierReportComponent,
    OnceOffItemsReportComponent,
    GLVATReportComponent,
    ChartsComponent,
    OrdersReportComponent,
    EmployeePositionComponent,
    StockCorrectionComponent,
    StockGroupComponent,
    ProductionStockComponent,    
    AllocateUsersComponent,
    DepartmentUsersComponent,
    FileManagementComponent,
    OrdersComponent,
    StorageTypeComponent,
    VatComponent,
    BirthdayComponent,
    PhonelistComponent,
    ImportFilesComponent,
    InvoiceComponent,
    InvoiceModifyComponent,
    GRNComponent,
    GRNModifyComponent,
    StockModifyComponent,
    OrdersModifyComponent,
    QuotationModifyComponent,
    QuotationComponent,
    OrdersReportViewComponent,
    StocktakeComponent,
    FinDashboardComponent,
    ProductionDashboardComponent,
    StocktakeCycleComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    NgbDropdownModule,
    NgbModalModule,
    PagesRoutingModule,
    NgApexchartsModule,
    ReactiveFormsModule,
    DxBoxModule,
    DashboardsModule,
    HttpClientModule,
    UIModule,
    WidgetModule,
    FullCalendarModule,
    NgbNavModule,
    NgbTooltipModule,
    NgbCollapseModule,
    SimplebarAngularModule,
    LightboxModule,
    DxChartModule,
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
    DxFileManagerModule,
    DxLinearGaugeModule,
    DxResponsiveBoxModule
  ],
  providers: [
    PriceIncreaseService,
    StockService,
    BarcodesService,
    QuarantineService,
    popupAutoSelectService,
    SupplierService,
    DashboardService,
    UserManagementService,
    PriceLookUpService,
    UserProfileService,
    OrdersService,
    ChartService,
    StocktakeService,
    QuotationService
  ],
})
export class PagesModule {}
