import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { User } from './../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private origin;
  public currentUser: Observable<User>;

  constructor(private http: HttpClient) {
  }

  register() {

  }

  login(Login: string, Password: string) {
    let headers = new HttpHeaders().set('Access-Control-Allow-Origin', this.origin);

    return this.http.post<any>(this.origin + "/api/Identity/login", { Login, Password }, { headers })
      .pipe(map(user => {
        if (user && user.token) {
          localStorage.setItem('currentUser', JSON.stringify(user));
        }
        return user;
      }));
  }

  logout() {
    localStorage.removeItem('currentUser');
  }

}
