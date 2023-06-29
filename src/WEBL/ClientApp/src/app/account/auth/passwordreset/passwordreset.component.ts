import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { AuthenticationService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-passwordreset',
  templateUrl: './passwordreset.component.html',
  styleUrls: ['./passwordreset.component.scss']
})

/**
 * Reset-password component
 */
export class PasswordresetComponent implements OnInit, AfterViewInit {

  // set the currenr year
  year: number = new Date().getFullYear();

  resetForm: FormGroup;
  submitted = false;
  error = '';
  success = '';
  loading = false;
  password;
  confirmPassword;
  isValid = true;
  account = '';
  reference = ''

  // tslint:disable-next-line: max-line-length
  constructor(private formBuilder: FormBuilder, private route: ActivatedRoute, private router: Router, private authenticationService: AuthenticationService) {
    this.route.queryParamMap.subscribe(queryParams => {
      this.account = queryParams.get('Account');
    });
    this.route.queryParamMap.subscribe(queryParams => {
      this.reference = queryParams.get('Reference');
    });
  }

  ngOnInit() {
    this.resetForm = this.formBuilder.group({
      password: ['', [Validators.required]],
      confirmPassword: ['', [Validators.required ]]
    });
  }

  ngAfterViewInit() {
  }

  // convenience getter for easy access to form fields
  get f() { return this.resetForm.controls; }

  /**
   * On submit form
   */
  onSubmit() {

    // Todo add as custom validator
    if (this.f.password.value !== this.f.confirmPassword.value) {

      this.isValid = false;
      return;
    }

    else {
      this.success = '';
      this.submitted = true;
      this.isValid = true;

      // stop here if form is invalid
      if (this.resetForm.invalid) {
        return;
      }

      this.authenticationService.SetPassword(this.account, this.f.password.value, this.reference)
          .pipe(first())
          .subscribe(
            data => {
              this.router.navigate(['/account/login']);
            },
            error => {
              console.log(error);
              this.error = error ? error : '';
            });
      
    }
  }
}
