import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { AuthenticationService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-landingpage',
  templateUrl: './landingpage.component.html',
  styleUrls: ['./landingpage.component.scss']
})

/**
 * Login component
 */
export class LandingPageComponent{

  loginForm: FormGroup;
  submitted = false;
  error = '';
  returnUrl: string;
  showPassword = false;
  password;
  attemptsRemaining = 3;

  // set the currenr year
  year: number = new Date().getFullYear();

  // tslint:disable-next-line: max-line-length
  constructor(private formBuilder: FormBuilder, private route: ActivatedRoute, private router: Router, private authenticationService: AuthenticationService) { }

  stores() {
    this.authenticationService.SetSite("store");
    this.router.navigate(['/stores/dashboard']);
  }

  acs() {
    this.authenticationService.SetSite("acs");
    this.router.navigate(['/dashboard']);
  }

/*  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required]],
      password: ['', [Validators.required]],
      rememberMe: [false, []],
    });

    // reset login status
    // this.authenticationService.logout();
    // get return url from route parameters or default to '/'
    // tslint:disable-next-line: no-string-literal
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';

    this.password = 'password';
  }*/

  /*onClick()
  {
    if (this.password === 'password') {
      this.password = 'text';
      this.showPassword = true;
    }
    else
    {
      this.password = 'password';
      this.showPassword = false
    }
  }*/

  // convenience getter for easy access to form fields
  //get f() { return this.loginForm.controls; }

  /**
   * Form submit
   */
  /*onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.loginForm.invalid) {
      return;
    } else {
      this.authenticationService.login(this.f.email.value, this.f.password.value, this.f.rememberMe.value)
          .pipe(first())
          .subscribe(
            data => {
              this.router.navigate(['/dashboard']);
            },
            error => {
              if (error == "Invalid Username/Email or Password") {
                this.attemptsRemaining = this.attemptsRemaining - 1;
                if (this.attemptsRemaining == 0) {
                  this.authenticationService.BlockUser(this.f.email.value).pipe(first()).subscribe(
                    data => {
                      this.router.navigate(['/account/user-blocked']);
                    }, error => {
                      this.error = error ? error : '';
                    });                  
                }
              }
              if (error == "User Disabled/Blocked") {
                this.router.navigate(['/account/user-blocked']);
              }
              this.error = error ? error : '';
            });

    }
  }*/
}
