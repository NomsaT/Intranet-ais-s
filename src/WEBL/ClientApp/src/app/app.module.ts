import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgbAccordionModule, NgbModule, NgbNavModule, NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';
import { NgIdleModule } from '@ng-idle/core';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { ScrollToModule } from '@nicky-lenaers/ngx-scroll-to';
import { CarouselModule } from 'ngx-owl-carousel-o';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ErrorInterceptor } from './core/helpers/error.interceptor';
import { JwtInterceptor } from './core/helpers/jwt.interceptor';
import { AuthenticationService } from './core/services/auth.service';
import { DashboardService } from './core/services/dashboard.service';
import { OrdersService } from './core/services/orders.service';
import { QuotationService } from './core/services/quotation.service';
import { PriceIncreaseService } from './core/services/priceIncrease.service';
import { PriceLookUpService } from './core/services/pricelookup.service';
import { DepartmentUserService } from './core/services/department-users.service';
import { StockService } from './core/services/stock.service';
import { BarcodesService } from './core/services/barcodes.service';
import { QuarantineService } from './core/services/quarantine.service';
import { popupAutoSelectService } from './core/services/popupAutoSelect.service';
import { SupplierService } from './core/services/supplier.service';
import { UserProfileService } from './core/services/user.service';
import { UserManagementService } from './core/services/usermanagement.service';
import { ExtrapagesModule } from './extrapages/extrapages.module';
import { LayoutsModule } from './layouts/layouts.module';
import { ChartService } from './core/services/chart.service';
import { StocktakeService } from './core/services/stocktake.service';

//import { Service, FileItem } from './app.service';

export function createTranslateLoader(http: HttpClient): any {
  return new TranslateHttpLoader(http, 'assets/i18n/', '.json');
}

@NgModule({
  declarations: [
    AppComponent,


  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: createTranslateLoader,
        deps: [HttpClient]
      }
    }),
    LayoutsModule,
    AppRoutingModule,
    ExtrapagesModule,
    CarouselModule,
    NgbAccordionModule,
    NgbNavModule,
    NgbTooltipModule,
    ScrollToModule.forRoot(),
    NgbModule,
    NgIdleModule.forRoot()
  ],
  bootstrap: [AppComponent],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    AuthenticationService,
    PriceIncreaseService,
    PriceLookUpService,
    StockService,
    BarcodesService,
    QuarantineService,
    popupAutoSelectService,
    SupplierService,
    DashboardService,
    UserManagementService,
    DepartmentUserService,
    UserProfileService,
    OrdersService,
    ChartService,
    StocktakeService,
    QuotationService
  ],
})
export class AppModule { }
//platformBrowserDynamic().bootstrapModule(AppModule);

