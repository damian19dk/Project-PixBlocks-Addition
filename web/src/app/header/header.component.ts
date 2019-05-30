import { Component, OnInit, Input } from '@angular/core';
import { AuthenticationService } from '../services/authentication.service';
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
    private authenticationService: AuthenticationService) { }

  ngOnInit() {
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  logout() {
    this.authenticationService.logout();
    //window.location.reload(); Reload strony, chwilowo niepotrzebny
    this.router.navigate([this.returnUrl]);
  }

  isUserLogged() {
    return this.authenticationService.isUserLogged();
  }

  getUserLogin() {
    return this.authenticationService.getUserLogin();
  }
}
