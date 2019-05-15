import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { User } from './../models/user.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private origin = environment.baseUrl;

  public currentUser: Observable<User>;

  constructor(private http: HttpClient) {
  }

  register(login: string, e_mail: string, password: string, roleId: number) {
    let headers = environment.headers;

    return this.http.post<any>(this.origin + "/api/Identity/register", { login, e_mail, password, roleId}, { headers })
      .pipe(map(user => {
        if (user && user.token) {
          localStorage.setItem('currentUser', JSON.stringify(user));
        }
        return user;
      }));
  }

  login(login: string, password: string) {
    let headers = environment.headers;

    return this.http.post<any>(this.origin + "/api/Identity/login", { login, password }, { headers })
      .pipe(map(user => {
        if (user && user.token) {
          localStorage.setItem('currentUser', JSON.stringify(user));
        }
        return user;
      }));
  }

  logout() {
    let headers = environment.headers;
    localStorage.removeItem('currentUser');

    return this.http.post<any>(this.origin + "/api/Identity/logout", { headers })
      .pipe(map(user => {
        if (user && user.token) {
          localStorage.setItem('currentUser', JSON.stringify(user));
        }
        return user;
      }));
  }

}
