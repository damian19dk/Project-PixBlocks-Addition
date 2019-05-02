import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { User } from './../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private loginUrl = 'https://localhost:5001/api/Identity/login';
  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;

  constructor(private http: HttpClient) { 
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  login(username: string, password: string) {
    let headers = new HttpHeaders().set('Access-Control-Allow-Origin',"https://localhost:5001/")
    return this.http.post<any>(this.loginUrl, { username, password },{ headers })
      .pipe(map(user => {
        if(user && user.token) {
          localStorage.setItem('currentUser', JSON.stringify(user));
          this.currentUserSubject.next(user);
        }
        return user;
      })).subscribe();
  }

  logout() {
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
}
}
