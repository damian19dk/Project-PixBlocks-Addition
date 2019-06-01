import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { User } from './../models/user.model';
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
        localStorage.removeItem("Token");
        this.loadingService.unload();
      },
      error => {
        localStorage.removeItem("Token");
        this.loadingService.unload();
      }
    );
  }

  refresh() {

    if (localStorage.getItem("Token") != undefined) {
      return this.http.post<any>(environment.baseUrl + "/api/Identity/refresh", localStorage.getItem("Token-Refresh"))
        .subscribe(
          data => {
            localStorage.setItem("Token", data.accessToken);
            localStorage.setItem("TokenRefresh", data.refreshToken)
            localStorage.setItem("TokenExpires", data.expires)
          },
          error => {

          }
        );
    }
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

}
