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


  constructor(private authenticationService: AuthenticationService, private router: Router) {
    this.isLoginValid = true;
    this.isEmailValid = true;
    this.isPasswordValid = true;
    this.isConfirmPasswordValid = true;
   }

  ngOnInit() {
  }

  signUp() {
    if(this.isValid()) {
      console.log("ZAREJESTRUJ")
    }
  }

  isValid() {
    let isError:boolean = false;

    // TODO Walidacja loginu
    if(this.Login.includes("\"")) {
      this.loginError = "Login nie może zawierać znaków: `,',\",<,>";
      this.isLoginValid = false;
      isError = true;
    }
    
    // TODO Walidacja hasła
    if(this.Password.length < 8) {
      this.passwordError = "Hasło musi mieć co najmniej 8 znaków, w tym jedną liczbę i wielką literę";
      this.isPasswordValid = false;
      isError = true;
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
