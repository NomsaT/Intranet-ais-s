import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { NgOtpInputModule } from 'ng-otp-input';
import { CarouselModule } from 'ngx-owl-carousel-o';
import { ExtrapagesRoutingModule } from './extrapages-routing.module';
import { Page404Component } from './page404/page404.component';

@NgModule({
  declarations: [ Page404Component ],
  imports: [
    CommonModule,
    CarouselModule,
    ExtrapagesRoutingModule,
    NgOtpInputModule
  ]
})
export class ExtrapagesModule { }
