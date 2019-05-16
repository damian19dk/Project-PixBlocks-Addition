import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({ templateUrl: 'registration.component.html' })
export class RegistrationComponent implements OnInit {

  registrationForm: FormGroup;
  loading: boolean;
  submitted: boolean;
  returnUrl: string;

  constructor(private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService) { }

  ngOnInit() {
    this.submitted = false;
    this.loading = false;

    this.registrationForm = this.formBuilder.group({
      username: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['',Validators.required]
    });

    this.authenticationService.logout();
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  get f() { return this.registrationForm.controls; }


  signUp() {
    this.submitted = true;

    if (this.registrationForm.invalid) {
      return;
    }

    this.loading = true;

    this.authenticationService.register(this.f.username.value, this.f.email.value, this.f.password.value, 3)
    // .pipe(first())
     .subscribe(
         data => {
             this.router.navigate([this.returnUrl]);
         },
         error => {
            // this.error = error;
             this.loading = false;
         });
  }
}
