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

  isLoginValid: boolean;
  isPasswordValid: boolean;

  loginError: string;
  passwordError: string;


  constructor(private authenticationService: AuthenticationService, private router: Router) { }

  ngOnInit() {
    this.isLoginValid = true;
    this.isPasswordValid = true;
  }

  signIn() {
    if(this.isValid()) {
      this.authenticationService.login(this.Login, this.Password);
    }
  }

  isValid() {
    let isError:boolean = false;

    if(this.Login.includes("`")
     || this.Login.includes("'")
     || this.Login.includes("\"")
     || this.Login.includes("<")
     || this.Login.includes(">")) {
      this.loginError = "Login nie może zawierać znaków: `,',\",<,>";
      this.isLoginValid = false;
      isError = true;
    }
    else {
      this.isLoginValid = true;
    }

    if(this.Password.length < 8) {
      this.passwordError = "Hasło musi mieć co najmniej 8 znaków";
      this.isPasswordValid = false;
      isError = true;
    }
    else {
      this.isPasswordValid = true;
    }
    
    if(!isError) {
      this.isPasswordValid = true;
      this.isLoginValid = true;
      return true;
    }
  }

}
