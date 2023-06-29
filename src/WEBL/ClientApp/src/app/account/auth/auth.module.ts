import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { CarouselModule } from 'ngx-owl-carousel-o';
import { UIModule } from '../../shared/ui/ui.module';
import { AuthRoutingModule } from './auth-routing';
import { ForgotPasswordComponent } from './forgotpassword/forgot-password.component';
import { LoginComponent } from './login/login.component';
import { LandingPageComponent } from './landingpage/landingpage.component';
import { PasswordresetComponent } from './passwordreset/passwordreset.component';
import { SetpasswordComponent } from './setpassword/setpassword.component';
import { SignupComponent } from './signup/signup.component';
import { SuccessResetComponent } from './successreset/success-reset.component';
import { UserBlockedComponent } from './userblocked/user-blocked.component';

@NgModule({
  declarations: [LoginComponent, SuccessResetComponent, SignupComponent, PasswordresetComponent, UserBlockedComponent, ForgotPasswordComponent, SetpasswordComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    NgbAlertModule,
    UIModule,
    AuthRoutingModule,
    CarouselModule
  ]
})
export class AuthModule { }
