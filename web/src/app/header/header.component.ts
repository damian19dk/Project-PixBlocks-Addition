import {Component, Input, OnInit} from '@angular/core';
import {AuthService} from '../services/auth.service';
import {ActivatedRoute, Router} from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  isNavCollapsed = true;
  selectedComponent: string;
  @Input() languages;
  private returnUrl: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthService) {
  }

  ngOnInit() {
    this.returnUrl = this.route.snapshot.queryParams.returnUrl || '/';
    this.selectedComponent = window.location.pathname.split('/').pop();
  }

  afterClick(selected: string): void {
    this.isNavCollapsed = true;
    this.selectedComponent = selected;
  }

  logout() {
    this.authenticationService.logout();
    this.router.navigate([this.returnUrl]);
  }

  isLogged() {
    return this.authenticationService.isLogged();
  }

  getLogin() {
    return this.authenticationService.getLogin();
  }
}
