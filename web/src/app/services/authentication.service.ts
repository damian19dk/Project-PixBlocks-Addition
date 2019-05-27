import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

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


    this.currentUser = new User();
    if (localStorage.getItem("Authorization") == undefined) {
      this.setUser(null, null, false);
    }
    else {
      this.setUser(localStorage.getItem("Login"), localStorage.getItem("Authorization"), true);
    }


  }

  register(login: string, e_mail: string, password: string, roleId: number) {

    return this.http.post<any>(this.origin + "/api/Identity/register", { login, e_mail, password, roleId });
  }

  login(login: string, password: string) {

    return this.http.post<any>(this.origin + "/api/Identity/login", { login, password });
  }

  logout() {
    this.loadingService.load();
    localStorage.removeItem("Authorization");
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
