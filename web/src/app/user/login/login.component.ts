import {Component, OnInit} from '@angular/core';
import {AuthService} from '../../services/auth.service';
import {ActivatedRoute, Router} from '@angular/router';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';

@Component({
  templateUrl: 'login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  form: FormGroup;
  loading: boolean;
  submitted: boolean;
  returnUrl: string;
  error: string;

  constructor(private formBuilder: FormBuilder,
              private route: ActivatedRoute,
              private router: Router,
              private authService: AuthService) { }

  ngOnInit() {
    this.submitted = false;
    this.loading = false;

    this.form = this.formBuilder.group({
      username: ['', [Validators.required, Validators.maxLength(20), Validators.pattern('[^ ]*')]],
      password: ['', [Validators.required, Validators.maxLength(20), Validators.pattern('[^ ]*')]]
    });

    this.returnUrl = this.route.snapshot.queryParams.returnUrl || '/';
  }

  get f() { return this.form.controls; }

  signIn() {
    this.submitted = true;

    if (this.form.invalid) {
      return;
    }

    this.loading = true;

    this.authService.login(this.f.username.value, this.f.password.value)
      .subscribe(
        data => {
          this.authService.addRolesToUser([data.roleName]);
          localStorage.setItem('Token', data.accessToken);
          localStorage.setItem('UserLogin', this.f.username.value);
          localStorage.setItem('TokenRefresh', data.refreshToken);
          localStorage.setItem('TokenExpires', data.expires);
          localStorage.setItem('UserRole', data.roleName);
          localStorage.setItem('UserEmail', data.email);
          localStorage.setItem('UserPremium', data.premium);
          this.router.navigate([this.returnUrl]);
        },
        error => {
          this.error = error;
          this.loading = false;
        });
  }
}
