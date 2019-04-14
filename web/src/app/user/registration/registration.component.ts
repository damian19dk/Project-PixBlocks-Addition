import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';
import { RegistrationData, ValidationError } from '../../services/user.model';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  registrationData: RegistrationData = new RegistrationData();
  error: ValidationError;

  constructor(private usersService: UserService, private router: Router) { }

  ngOnInit() {
  }

  signUp() {
    this.usersService.register(this.registrationData).subscribe(
      success => this.router.navigate(['login']),
      error => this.error = error.error
    );
  }

}
