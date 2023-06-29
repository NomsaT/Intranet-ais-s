import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from '.././account/auth/login/login.component';
import { DefaultStoresComponent } from './dashboards/default/default-stores.component';
import { BarcodeListComponent } from './barcode-list/barcode-list.component';
import { ReprintBarcodeListComponent } from './barcode-list-reprint/barcode-list-reprint.component';
import { BarcodeVerificationReportComponent } from './barcode-verification-report/barcode-verification-report.component';
import { BinOverviewComponent } from './bin-overview/bin-overview.component';

const routes: Routes = [
  { path: '', redirectTo: 'dashboard' },
  { path: 'dashboard', component: DefaultStoresComponent },
  { path: 'print-barcode', component: BarcodeListComponent },
  { path: 'reprint-barcode', component: ReprintBarcodeListComponent },
  { path: 'bin-overview', component: BinOverviewComponent },
  { path: 'barcode-verification-report', component: BarcodeVerificationReportComponent },
  { path: '/account/login', component: LoginComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StoresRoutingModule { }
