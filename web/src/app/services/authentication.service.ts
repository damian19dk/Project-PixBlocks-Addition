import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { environment } from '../../environments/environment';
import { LoadingService } from './loading.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(
    private http: HttpClient,
    private loadingService: LoadingService) {
  }

  register(login: string, e_mail: string, password: string, roleId: number) {
    let headers = new HttpHeaders()
      .set("Content-Type", "application/json");

    return this.http.post<any>(environment.baseUrl + "/api/Identity/register", { login, e_mail, password, roleId }, { headers });
  }

  login(login: string, password: string) {
    let headers = new HttpHeaders()
      .set("Content-Type", "application/json");

    return this.http.post<any>(environment.baseUrl + "/api/Identity/login", { login, password }, { headers });
  }

  logout() {
    this.loadingService.load();

    return this.http.post<any>(environment.baseUrl + "/api/Identity/cancel", {})
      .subscribe(
        data => {
          this.clearUserData();
          this.loadingService.unload();
        },
        error => {
          this.clearUserData();
          this.loadingService.unload();
        }
      );
  }

  refreshToken() {
    return this.http.post<any>(environment.baseUrl + "/api/Identity/refresh?token=" + localStorage.getItem("TokenRefresh"), {});
  }

  isLogged() {
    return localStorage.getItem("Token") != undefined;
  }

  getLogin() {
    return localStorage.getItem("Login");
  }

  getToken() {
    return localStorage.getItem("Token");
  }

  isTokenExpired() {
    return Date.now() > (parseInt(localStorage.getItem("TokenExpires")) * 1000);
  }

  clearUserData() {
    localStorage.removeItem("Token");
    localStorage.removeItem("TokenRefresh");
    localStorage.removeItem("TokenExpires");
    localStorage.removeItem("Login");
  }

}
