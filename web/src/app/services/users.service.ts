import { Injectable } from '@angular/core';
import { ApiHelperService } from './api-helper.service'
import { Subject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import {RegistrationData} from './user.model';

export class UserLoginData {
  client_secret: string = '2ahj6CXgRvietdgzJ4iNPaxlsseP2VIOiHh6bLxO';
  client_id: number = 1;
  grant_type: string = 'password';

  constructor(public username?: string, public password?: string) {}
}

export class User {
  constructor(public id: string, public username: string, public email: string) {}
}

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  currentUser: Subject<User> = new Subject();

  constructor(private apiHelper: ApiHelperService) {
    if(localStorage.getItem('token')) {
      this.refreshCurrentUser();
    }
  }

  get currentUser$() {
    return this.currentUser.asObservable();
  }

  login(userLoginData: UserLoginData): Observable<any> {
    return this.apiHelper.post('oauth/token', userLoginData).pipe( tap( data => {
      this.refreshCurrentUser();
      localStorage.setItem('token', data.access_token);
    }));
  }
  register(registrationData: RegistrationData): Observable<any> {
    return this.apiHelper.post('user/register', registrationData);
  }

  refreshCurrentUser() {
    this.apiHelper.get('user/data').subscribe(result => this.currentUser.next(result.data))
  }
}
