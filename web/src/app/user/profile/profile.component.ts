import { Component, OnInit } from '@angular/core';
import { UserDocument } from 'src/app/models/userDocument.model';
import { AuthService } from 'src/app/services/auth.service';

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
    this.userDocument.email = this.authService.getEmail();
    this.userDocument.roleId = this.convertRoleIdToRoleName(this.authService.getUserRole());
  }

  convertRoleIdToRoleName(id: string) {
    let roleName = "";
    switch(parseInt(id)) {
      case 1:
        roleName = "Użytkownik"
      break;
      case 2:
        roleName = "Administrator";
      break;
      default:
      break;
    }
    return roleName;
  }


}
