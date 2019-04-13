import { Component, OnInit } from '@angular/core';
import { UsersService } from '../../services/users.service';
import { Router } from '@angular/router';
import { LoginData } from '../../services/user.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginData: LoginData = new LoginData();
  loginError = false;

  constructor(private usersService: UsersService, private router: Router) { }

  ngOnInit() { }

  signIn() {
    this.usersService.login(this.loginData).subscribe(
      success => this.router.navigate(['/profile']), 
      error => this.loginError = true
    );
  }

}
