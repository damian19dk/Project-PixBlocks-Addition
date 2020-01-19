import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {FormBuilder, Validators} from '@angular/forms';
import {NgbModal, NgbModalConfig} from '@ng-bootstrap/ng-bootstrap';
import {UserService} from 'src/app/services/user.service';
import {AuthService} from 'src/app/services/auth.service';
import {FormModal} from '../../../models/formModal';

@Component({
  selector: 'app-change-email',
  templateUrl: './change-email.component.html',
  styleUrls: ['./change-email.component.css']
})
export class ChangeEmailComponent extends FormModal implements OnInit {
  @Output() profileChanged: EventEmitter<any> = new EventEmitter<any>();

  constructor(protected modalService: NgbModal,
              private formBuilder: FormBuilder,
              private userService: UserService,
              private authService: AuthService,
              protected modalConfig: NgbModalConfig) {
    super(modalService, modalConfig);
  }

  ngOnInit() {
    this.initFormModal();
  }

  initFormModal() {
    this.form = this.formBuilder.group({
      email: [null, [Validators.required, Validators.email]]
    });
  }

  changeEmail() {
    this.submitted = true;

    if (this.form.invalid) {
      return;
    }

    this.loading = true;
    const login = this.authService.getLogin();

    return this.userService.changeEmail(login, this.form.value.email).subscribe(
      data => {
        localStorage.setItem('UserEmail', this.form.value.email);
        this.emitProfileChangedEvent();
        this.sent = true;
        this.error = null;
        this.loading = false;
      },
      error => {
        this.sent = true;
        this.error = error;
        this.loading = false;
      }
    );
  }

  emitProfileChangedEvent() {
    this.profileChanged.emit(null);
  }
}
