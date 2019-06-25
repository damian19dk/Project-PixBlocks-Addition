import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { UserService } from 'src/app/services/user.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {

  changePasswordForm: FormGroup;

  constructor(private formBuilder: FormBuilder,
    private userService: UserService,
    private authService: AuthService) { }

  ngOnInit() {
    this.changePasswordForm = this.formBuilder.group({
      oldPassword: [null],
      newPassword: [null]
    });
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

}
