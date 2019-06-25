import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { LoadingService } from './loading.service';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

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
    return localStorage.getItem("UserLogin");
  }

  getToken() {
    return localStorage.getItem("Token");
  }

  getUserRole() {
    return localStorage.getItem("UserRole");
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
    localStorage.removeItem("UserLogin");
    localStorage.removeItem("UserRole");
  }

  setUserData(token: string, tokenRefresh: string, tokenExpires: string, login: string, roleId: string) {
    localStorage.setItem("Token", token);
    localStorage.setItem("TokenRefresh", tokenRefresh);
    localStorage.setItem("TokenExpires", tokenExpires);
    localStorage.setItem("UserLogin", login);
    localStorage.setItem("UserRole", roleId);
  }

}
