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

  form: FormGroup;

  constructor(private modalService: NgbModal,
    private formBuilder: FormBuilder,
    private userService: UserService,
    private authService: AuthService) { }

  ngOnInit() {
    this.form = this.formBuilder.group({
      email: [null]
    });
  }

  get f() { return this.form.controls; }


  changeEmail() {
    this.submitted = true;

    if (this.form.invalid) {
      return;
    }

    this.loading = true;

    let login = this.authService.getLogin();

    return this.userService.changeEmail(login, this.form.value.email).subscribe(
      data => {
        localStorage.setItem("UserEmail", this.form.value.email);
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
