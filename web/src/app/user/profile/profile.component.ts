import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { UserDocument } from 'src/app/models/userDocument.model';
import { UserService } from 'src/app/services/user.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  userDocument: UserDocument;
  changePasswordForm: FormGroup;
  changeEmailForm: FormGroup;

  constructor(private authService: AuthService,
              private userService: UserService,
              private modalService: NgbModal,
              private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.changePasswordForm = this.formBuilder.group({
      oldPassword: [null],
      newPassword: [null]
    });

    this.changeEmailForm = this.formBuilder.group({
      email: [null]
    });

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

  changePassword() {
    if (this.changePasswordForm.invalid) {
      return;
    }

    let login = this.authService.getLogin();

    return this.userService.changePassword(login, this.changePasswordForm.value.newPassword, this.changePasswordForm.value.oldPassword).subscribe(
      data => {

      },
      error => {

      }
    );
  }

  changeEmail() {
    if (this.changeEmailForm.invalid) {
      return;
    }

    let login = this.authService.getLogin();

    return this.userService.changeEmail(login, this.changeEmailForm.value.email).subscribe(
      data => {

      },
      error => {

      }
    );
  }

  openModal(content) {
    this.modalService.open(content, { centered: true });
  }
}
