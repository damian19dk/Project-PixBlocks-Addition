import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { UserService } from 'src/app/services/user.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-change-email',
  templateUrl: './change-email.component.html',
  styleUrls: ['./change-email.component.css']
})
export class ChangeEmailComponent implements OnInit {

  error: string;
  loading: boolean;
  submitted: boolean;
  sent: boolean;

  changeEmailForm: FormGroup;

  constructor(private modalService: NgbModal,
    private formBuilder: FormBuilder,
    private userService: UserService,
    private authService: AuthService) { }

  ngOnInit() {
    this.changeEmailForm = this.formBuilder.group({
      email: [null]
    });
  }

  get f() { return this.changeEmailForm.controls; }


  changeEmail() {
    this.submitted = true;

    if (this.changeEmailForm.invalid) {
      return;
    }

    this.loading = true;

    let login = this.authService.getLogin();

    return this.userService.changeEmail(login, this.changeEmailForm.value.email).subscribe(
      data => {
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

  openModal(content) {
    this.modalService.open(content, { centered: true });
  }

}
