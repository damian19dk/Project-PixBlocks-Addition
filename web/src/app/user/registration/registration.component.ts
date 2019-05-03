import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  Login: string;
  Email: string;
  Password: string;
  ConfirmPassword: string;

  loginError: string;
  emailError: string;
  passwordError: string;
  confirmPasswordError: string;

  isLoginValid: boolean;
  isEmailValid: boolean;
  isPasswordValid: boolean;
  isConfirmPasswordValid: boolean;


  constructor(private authenticationService: AuthenticationService, private router: Router) {  }

  ngOnInit() {
    this.isLoginValid = true;
    this.isEmailValid = true;
    this.isPasswordValid = true;
    this.isConfirmPasswordValid = true;
  }

  signUp() {
    if(this.isValid()) {
      //this.authenticationService.register();
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

    // TODO Walidacja email
    if(!this.Email.includes("@")) {
      this.emailError = "Nieprawidłowa forma email";
      this.isEmailValid = false;
      isError = true;
    }
    else {
      this.isEmailValid = true;
    }
    
    if(this.Password.length < 8) {
      this.passwordError = "Hasło musi mieć co najmniej 8 znaków";
      this.isPasswordValid = false;
      isError = true;
    }
    else {
      this.isPasswordValid = true;
    }

    if(this.ConfirmPassword != this.Password) {
      this.confirmPasswordError = "Źle powtórzone hasło";
      this.isConfirmPasswordValid = false;
      isError = true;
    }
    else {
      this.isConfirmPasswordValid = true;
    }

    
    if(!isError) {
      this.isLoginValid = true;
      this.isEmailValid = true;
      this.isPasswordValid = true;
      this.isConfirmPasswordValid = true;

      return true;
    }
  }

}
