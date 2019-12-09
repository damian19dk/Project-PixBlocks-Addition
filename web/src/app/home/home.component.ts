import {Component, OnInit} from '@angular/core';
import {AuthService} from '../services/auth.service';
import {LoadingService} from '../services/loading.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  count: number;
  page = 1;

  error: string;

  constructor(
    private loadingService: LoadingService,
    private authenticationService: AuthService) {
  }

  ngOnInit() {
  }

  isLogged() {
    return this.authenticationService.isLogged();
  }

  getLogin() {
    return this.authenticationService.getLogin();
  }

}
