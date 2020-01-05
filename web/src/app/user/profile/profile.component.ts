import {Component, OnInit} from '@angular/core';
import {UserDocument} from 'src/app/models/userDocument.model';
import {AuthService} from 'src/app/services/auth.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  userDocument: UserDocument;

  constructor(private authService: AuthService) {
    this.userDocument = new UserDocument();
  }

  ngOnInit() {
    this.getUser();
  }

  getUser() {
    this.userDocument.login = this.authService.getLogin();
    this.userDocument.email = this.authService.getEmail();
    this.userDocument.roleName = this.authService.getUserRole();
  }
}
