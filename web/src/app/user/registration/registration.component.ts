import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MustMatch } from './must-match.validator';

@Component({ templateUrl: 'registration.component.html' })
export class RegistrationComponent implements OnInit {

  registrationForm: FormGroup;
  loading: boolean;
  submitted: boolean;
  returnUrl: string;
  error: string;

  constructor(private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService) { }

  ngOnInit() {
    this.submitted = false;
    this.loading = false;

    this.registrationForm = this.formBuilder.group({
      username: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(20), Validators.pattern('[^ ]*')]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(20), Validators.pattern('[^ ]*')]],
      confirmPassword: ['', Validators.required]
    }, {
        validator: MustMatch('password', 'confirmPassword')
      });

    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || 'login';
  }

  get f() { return this.registrationForm.controls; }


  signUp() {
    this.submitted = true;

    if (this.registrationForm.invalid) {
      return;
    }

    this.loading = true;

    this.authenticationService.register(this.f.username.value, this.f.email.value, this.f.password.value, 2)
      .subscribe(
        data => {
          this.router.navigate([this.returnUrl]);
        },
        error => {
          this.error = error.error == null ? null : error.error.message;
          this.loading = false;
        }
      );
  }




}
