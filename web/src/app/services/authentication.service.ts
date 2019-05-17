import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';

import { User } from './../models/user.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private origin = environment.baseUrl;
  private currentUser: User;

  constructor(
    private http: HttpClient) {
      this.currentUser = new User();
      this.currentUser.login = null;
      this.currentUser.token = null;
      this.currentUser.isLogged = false;
  }

  register(login: string, e_mail: string, password: string, roleId: number) {
    let headers = environment.headers;

    return this.http.post<any>(this.origin + "/api/Identity/register", { login, e_mail, password, roleId }, { headers })
      .pipe(map(user => {
        if (user && user.token) {

          localStorage.setItem('X-Auth-Token', JSON.stringify(user));
        }
        return user;
      }));
  }

  login(login: string, password: string) {
    let headers = environment.headers;

    return this.http.post<any>(this.origin + "/api/Identity/login", { login, password }, { headers })
      .pipe(map(user => {
          this.setUser(login, user.token, true);

          localStorage.setItem("X-Auth-Token", this.currentUser.token);
          console.log('Token logowania: ' + localStorage.getItem("X-Auth-Token"));
        return user;
      }));
  }

  logout() {
    localStorage.removeItem('X-Auth-Token');
    this.setUser(null, null, false);
  }

  IsUserLogged() {
    return this.currentUser.isLogged;
  }

  setUser(login: string, token: string, isLogged: boolean) {
    this.currentUser.login = login;
    this.currentUser.token = token;
    this.currentUser.isLogged = isLogged;
  }

}
