import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  Login: string;
  Password: string;

  isError: boolean;
  error: string;

  constructor(private authenticationService: AuthenticationService, private router: Router) { }

  ngOnInit() {
    this.isError = false;
  }

  signIn() {
    if(this.validate()) {
      this.authenticationService.login(this.Login, this.Password);
    }
  }

  validate() {
    if(this.Password.length < 8) {
      this.isError = true;
      this.error = "Hasło musi mieć przynajmniej 8 znaków";
      return false;
    }
    
    else {
      this.isError = false;
      this.error = "";
      return true;
    }
  }

}
