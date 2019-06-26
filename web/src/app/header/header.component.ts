import { Component, OnInit, Input } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  isNavbarCollapsed = true;
  public isCollapsed = false;
  @Input() languageList;
  private returnUrl: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthService) { }

  ngOnInit() {
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
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

  getUserRole() {
    return this.authenticationService.getUserRole();
  }

}
