import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { UserDocument } from 'src/app/models/userDocument.model';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  userDocument: UserDocument;

  constructor(private authService: AuthService) { }

  ngOnInit() {
    this.getUser();
  }

  getUser() {
    this.userDocument = new UserDocument();
    this.userDocument.login = this.authService.getLogin();
    this.userDocument.email = "example@mail.com";
    this.userDocument.roleId = parseInt(this.authService.getUserRole());
  }

  changePassword() {

  }

  changeEmail() {
    
  }



}
