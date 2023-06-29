import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { AuthenticationService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-setpassword',
  templateUrl: './setpassword.component.html',
  styleUrls: ['./setpassword.component.scss']
})

/*
Reset-password component
 */
export class SetpasswordComponent implements OnInit, AfterViewInit {

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

  // tslint:disable-next-line: max-line-length
  constructor(private formBuilder: FormBuilder, private route: ActivatedRoute, private router: Router, private authenticationService: AuthenticationService) {
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

/*  
  On submit form
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
      else {
        this.authenticationService.UserSetPassword(this.f.password.value)
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
}

