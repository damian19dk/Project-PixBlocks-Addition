import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { environment } from '../../environments/environment';
import { LoadingService } from './loading.service';
import { retry } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private isTokenRefreshing: boolean = false;

  constructor(
    private http: HttpClient,
    private loadingService: LoadingService) {
    setInterval(() => this.autoRefresh(), 24000) // 30000ms = 5min
  }

  autoRefresh() {
    if (!this.isTokenRefreshing && this.isLogged() && this.isTokenExpired()) {
      this.isTokenRefreshing = true;
      this.refreshToken().subscribe(
        data => {
          localStorage.setItem("Token", data.accessToken);
          localStorage.setItem("TokenRefresh", data.refreshToken);
          localStorage.setItem("TokenExpires", data.expires);
          this.isTokenRefreshing = false;
        }
      )
    }
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

    // this.cancelToken()
    //   .subscribe(
    //     data => {
    //       this.clearUserData();
    //       this.loadingService.unload();
    //     },
    //     error => {
    //       this.clearUserData();
    //       this.loadingService.unload();
    //     }
    //   );
    this.clearUserData();
    this.loadingService.unload();
  }

  cancelToken() {
    return this.http.post<any>(environment.baseUrl + "/api/Identity/cancel", {});
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

  getExpirationTime() {
    return localStorage.getItem("TokenExpires") == undefined ? 5000 : (parseInt(localStorage.getItem("TokenExpires")) - Date.now());
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

  setUserData(token: string, tokenRefresh: string, tokenExpires: string, login: string) {
    localStorage.setItem("Token", token);
    localStorage.setItem("TokenRefresh", tokenRefresh);
    localStorage.setItem("TokenExpires", tokenExpires);
    localStorage.setItem("Login", login);
  }

}
