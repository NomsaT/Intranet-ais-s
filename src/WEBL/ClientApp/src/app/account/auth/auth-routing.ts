import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ForgotPasswordComponent } from './forgotpassword/forgot-password.component';
import { InvalidLinkComponent } from './invalidlink/invalid-link.component';
import { LoginComponent } from './login/login.component';
import { LandingPageComponent } from './landingpage/landingpage.component';
import { PasswordresetComponent } from './passwordreset/passwordreset.component';
import { SetpasswordComponent } from './setpassword/setpassword.component';
import { SignupComponent } from './signup/signup.component';
import { SuccessResetComponent } from './successreset/success-reset.component';
import { UserBlockedComponent } from './userblocked/user-blocked.component';

const routes: Routes = [
    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: 'signup',
        component: SignupComponent
    },
    {
        path: 'success-reset',
      component: SuccessResetComponent
    },
    {
        path: 'reset-password',
        component: PasswordresetComponent
    },
    {
        path: 'forgot-password',
        component: ForgotPasswordComponent
    },
    {
        path: 'user-blocked',
      component: UserBlockedComponent
    },
    {
      path: 'invalid-link',
      component: InvalidLinkComponent
    },
    {
      path: 'setpassword',
      component: SetpasswordComponent
    },
    {
      path: 'landingpage',
      component: LandingPageComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AuthRoutingModule { }
