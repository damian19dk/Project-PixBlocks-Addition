import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  username: string;
  password: string;
  loginError: boolean;

  constructor(private authenticationService: AuthenticationService, private router: Router) { }

  ngOnInit() {
    this.loginError = false;
  }

  signIn() {
    this.authenticationService.login(this.username, this.password);
  }

}
