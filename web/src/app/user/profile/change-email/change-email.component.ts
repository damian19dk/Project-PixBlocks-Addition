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

}
