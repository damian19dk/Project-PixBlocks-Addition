import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  changePassword(login: string, newPassword: string, oldPassword: string) {
    const headers = new HttpHeaders();
    return this.http.put<any>(environment.baseUrl + '/api/User/passwordChange?login=' + login + '&newPassword=' + newPassword + '&oldPassword=' + oldPassword, { }, { headers });
  }

  changeEmail(login: string, email: string) {
    const headers = new HttpHeaders();
    return this.http.put<any>(environment.baseUrl + '/api/User/emailChange?login=' + login + '&email=' + email, { }, { headers });
  }
}
