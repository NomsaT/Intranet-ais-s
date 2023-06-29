import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from '.././account/auth/login/login.component';
import { AllocateUsersComponent } from './allocate-users/allocate-users.component';
import { AssignRolesComponent } from './assign-roles/assign-roles.component';
import { CustomerComponent } from './customer/customer.component';
import { DefaultComponent } from './dashboards/default/default.component';
import { DepartmentManagerComponent } from './department-managers/department-managers.component';
import { DepartmentComponent } from './department/department.component';
import { EmployeePositionComponent } from './employee-position/employee-position.component';
import { FileManagementComponent } from './file-management/file-management.component';
import { ImportFilesComponent } from './import-files/import-files.component';
import { InvoiceModifyComponent } from './invoices-modify/invoices-modify.component';
import { GRNModifyComponent } from './grn-modify/grn-modify.component';
import { OrdersModifyComponent } from './orders-modify/orders-modify.component';
import { StockModifyComponent } from './stocks-modify/stocks-modify.component';
import { OrdersReportViewComponent } from './orders-report-view/orders-report-view.component';
import { InvoiceComponent } from './invoices/invoices.component';
import { GRNComponent } from './grn/grn.component';
import { JobComponent } from './job/job.component';
import { OrdersReportComponent } from './orders-report/orders-report.component';
import { OrdersComponent } from './orders/orders.component';
import { PaymentMethodComponent } from './payment-methods/payment-methods.component';
import { PermissionsComponent } from './permissions/permissions.component';
import { PlantLocationComponent } from './plant-location/plant-location.component';
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
import { ProductsComponent } from './products/products.component';
import { ProductsSummaryComponent } from './products-summary/products-summary.component';
import { StockRecipesComponent } from './stock-recipe/stock-recipe.component';
import { DepartmentStockComponent } from './department-stock/department-stock.component';
import { StockTransferComponent } from './stock-transfer/stock-transfer.component';
import { RolesComponent } from './roles/roles.component';
import { SettingsComponent } from './settings/settings.component';
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
import { StocktakeComponent } from './stocktake/stocktake.component';
import { FinDashboardComponent } from './fin-dashboard/fin-dashboard.component';
import { StocktakeCycleComponent } from './stocktake-cycle/stocktake-cycle.component';
import { QuotationComponent } from './quotation/quotation.component';
import { QuotationModifyComponent } from './quotation-modify/quotation-modify.component';
import { ProductionDashboardComponent } from './production-dashboard/production-dashboard.component';

const routes: Routes = [
  { path: '', redirectTo: 'dashboard' },
  { path: 'dashboard', component: DefaultComponent },
  { path: 'customers', component: CustomerComponent },
  { path: 'stocklist', component: StocklistComponent },
  { path: 'suppliers', component: SupplierComponent },
  { path: 'department-managers', component: DepartmentManagerComponent },
  { path: 'department', component: DepartmentComponent },
  { path: 'products', component: ProductsComponent },
  { path: 'orders', component: OrdersComponent },
  { path: 'allocate-users', component: AllocateUsersComponent },
  { path: 'department-stock', component: DepartmentStockComponent },
  { path: 'stock-transfer', component: StockTransferComponent },
  { path: 'production-stock', component: ProductionStockComponent },
  { path: 'price-lookup', component: PriceLookUpComponent },
  { path: 'unit-of-measurement', component: UnitOfMeasurementComponent },
  { path: 'employee-position', component: EmployeePositionComponent },
  { path: 'job', component: JobComponent },
  { path: 'stock-report', component: StockReportComponent },
  { path: 'approved-orders-report', component: OrdersReportComponent },
  { path: 'users', component: UsersComponent },
  { path: 'roles', component: RolesComponent },
  { path: 'permissions', component: PermissionsComponent },
  { path: 'assign-roles', component: AssignRolesComponent },
  { path: 'settings', component: SettingsComponent },
  { path: 'store-type', component: StoreTypeComponent },
  { path: 'gl-code', component: GLCodeComponent },
  { path: 'bank-name', component: BankNameComponent },
  { path: 'project', component: ProjectComponent },
  { path: 'company', component: CompanyComponent },
  { path: 'bins', component: BinsComponent },
  { path: 'project-costing', component: ProjectCostingComponent },
  { path: 'return-to-supplier', component: ReturnToSupplierComponent },
  { path: 'cost-type', component: CostTypesComponent },
  { path: 'plant-location', component: PlantLocationComponent },
  { path: 'stock-category', component: StockCategoryComponent },
  { path: 'payment-method', component: PaymentMethodComponent },
  { path: 'stock-group', component: StockGroupComponent },
  { path: 'storage-type', component: StorageTypeComponent },
  { path: 'update-vat', component: VatComponent },
  { path: 'birthdays', component: BirthdayComponent },
  { path: 'phonelist', component: PhonelistComponent },
  { path: 'stock-correction', component: StockCorrectionComponent },
  { path: '/account/login', component: LoginComponent },
  { path: 'file-management', component: FileManagementComponent },
  { path: 'import-files', component: ImportFilesComponent },
  { path: 'invoices', component: InvoiceComponent },
  { path: 'invoices-modify', component: InvoiceModifyComponent },
  { path: 'grn', component: GRNComponent },
  { path: 'grn-modify', component: GRNModifyComponent },
  { path: 'orders-modify', component: OrdersModifyComponent },
  { path: 'stocks-modify', component: StockModifyComponent },
  { path: 'orders-report-view', component: OrdersReportViewComponent },
  { path: 'service-items-report', component: ServiceItemsReportComponent },
  { path: 'return-to-supplier-report', component: ReturnToSupplierReportComponent },
  { path: 'once-off-items-report', component: OnceOffItemsReportComponent },
  { path: 'gl-vat-report', component: GLVATReportComponent },
  { path: 'charts', component: ChartsComponent },
  { path: 'stock-recipes', component: StockRecipesComponent },  
  { path: 'fin-dashboard', component: FinDashboardComponent },
  { path: 'production-dashboard', component: ProductionDashboardComponent },
  { path: 'product-summary', component: ProductsSummaryComponent },
  { path: 'stocktake', component: StocktakeComponent },
  { path: 'stocktake-cycle', component: StocktakeCycleComponent },
  { path: 'quotation', component: QuotationComponent},
  { path: 'quotation-modify', component: QuotationModifyComponent},
  { path: 'quarantine-stock', component: DepartmentStockComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PagesRoutingModule { }