import {Component, OnInit} from '@angular/core';
import {AuthService} from '../services/auth.service';
import {LoadingService} from '../services/loading.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  error: string;
  historyCoursesNumber = 0;

  constructor(
    private loadingService: LoadingService,
    private authService: AuthService) {
  }

  ngOnInit() {
  }

  isLogged() {
    return this.authService.isLogged();
  }

  getLogin() {
    return this.authService.getLogin();
  }

  getHistoryCoursesNumber($event) {
    this.historyCoursesNumber = $event;
  }
}
