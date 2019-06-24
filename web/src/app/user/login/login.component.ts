import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
    templateUrl: 'login.component.html',
    styleUrls: ['./login.component.css'] 
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  loading: boolean;
  submitted: boolean;
  returnUrl: string;
  error: string;

  constructor(private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthService) { }

  ngOnInit() {
    this.submitted = false;
    this.loading = false;

    this.loginForm = this.formBuilder.group({
      username: ['', [Validators.required, Validators.maxLength(20), Validators.pattern('[^ ]*')]],
      password: ['', [Validators.required, Validators.maxLength(20), Validators.pattern('[^ ]*')]]
    });

    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  get f() { return this.loginForm.controls; }

  signIn() {
    this.submitted = true;

    if (this.loginForm.invalid) {
      return;
    }

    this.loading = true;

    this.authenticationService.login(this.f.username.value, this.f.password.value)
      .subscribe(
        data => {
          localStorage.setItem("Token", data.accessToken);
          localStorage.setItem("Login", this.f.username.value);
          localStorage.setItem("TokenRefresh", data.refreshToken);
          localStorage.setItem("TokenExpires", data.expires);                 
          this.router.navigate([this.returnUrl]);
        },
        error => {
          this.error = error;
          this.loading = false;
        });
  }
}