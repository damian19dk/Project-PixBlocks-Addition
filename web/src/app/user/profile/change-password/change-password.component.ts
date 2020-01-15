import {Component, OnInit} from '@angular/core';
import {FormBuilder, Validators} from '@angular/forms';
import {UserService} from 'src/app/services/user.service';
import {AuthService} from 'src/app/services/auth.service';
import {NgbModal, NgbModalConfig} from '@ng-bootstrap/ng-bootstrap';
import {FormModal} from '../../../models/formModal';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent extends FormModal implements OnInit {

  constructor(protected modalService: NgbModal,
              protected modalConfig: NgbModalConfig,
              private formBuilder: FormBuilder,
              private userService: UserService,
              private authService: AuthService) {
    super(modalService, modalConfig);
  }

  ngOnInit() {
    this.initFormModal();
  }

  initFormModal() {
    this.form = this.formBuilder.group({
      oldPassword: [null, Validators.required],
      newPassword: [null, [Validators.required, Validators.minLength(6), Validators.maxLength(20), Validators.pattern('[^ ]*')]],
    });
  }

  changePassword() {
    this.submitted = true;

    if (this.form.invalid) {
      return;
    }

    const login = this.authService.getLogin();
    return this.userService.changePassword(login, this.form.value.newPassword, this.form.value.oldPassword).subscribe(
      data => {
        this.error = null;
      },
      error => {
        this.error = error;
      }
    );
  }
}
