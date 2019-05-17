import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';

import { User } from './../models/user.model';
import { environment } from '../../environments/environment';
import { LoadingService } from './loading.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private origin = environment.baseUrl;
  private currentUser: User;

  constructor(
    private http: HttpClient,
    private loadingService: LoadingService) {


    this.currentUser = JSON.parse(localStorage.getItem("currentUser"));
    if (this.currentUser == null) {
      this.currentUser = new User();
      this.setUser(null, null, false);
    }
  }

  register(login: string, e_mail: string, password: string, roleId: number) {
    let headers = environment.headers;

    return this.http.post<any>(this.origin + "/api/Identity/register", { login, e_mail, password, roleId }, { headers })
      .pipe(map(user => {
        if (user && user.token) {

          localStorage.setItem("currentUser", JSON.stringify(this.currentUser));
        }
        return user;
      }));
  }

  login(login: string, password: string) {

    let headers = environment.headers;

    return this.http.post<any>(this.origin + "/api/Identity/login", { login, password }, { headers })
      .pipe(map(user => {

        this.setUser(login, user.token, true);

        localStorage.setItem("currentUser", JSON.stringify(this.currentUser));
        
        return user;
      }));
  }

  logout() {
    this.loadingService.load();
    localStorage.removeItem("currentUser");
    this.setUser(null, null, false);
    this.loadingService.unload();
  }

  isUserLogged() {
    return this.currentUser.isLogged;
  }

  getUserLogin() {
    return this.currentUser.login;
  }

  setUser(login: string, token: string, isLogged: boolean) {
    this.currentUser.login = login;
    this.currentUser.token = token;
    this.currentUser.isLogged = isLogged;
  }

}
