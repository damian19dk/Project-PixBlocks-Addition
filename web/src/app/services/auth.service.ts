import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {environment} from '../../environments/environment';
import {LoadingService} from './loading.service';
import {BehaviorSubject, Observable, ReplaySubject} from 'rxjs';
import {scan, takeWhile} from 'rxjs/operators';
import {JwtHelperService} from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  roles$ = new ReplaySubject<string[]>(1);
  roleNames = ['user', 'admin'];
  roleUpdates$ = new BehaviorSubject([this.getUserRoleName()]);

  private isTokenRefreshing = false;

  constructor(
    private http: HttpClient,
    private loadingService: LoadingService,
    private jwtHelper: JwtHelperService) {

    this.roleUpdates$
      .pipe(scan((acc, next) => next, []))
      .subscribe(this.roles$);

    setInterval(() => this.autoRefresh(), 10000); // 30000ms = 5min
  }

  // tslint:disable-next-line:variable-name
  register(login: string, e_mail: string, password: string, roleId: number): Observable<any> {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json');

    return this.http.post<any>(environment.baseUrl + '/api/Identity/register', {
      login,
      e_mail,
      password,
      roleId
    }, {headers});
  }

  addRolesToUser(roles: Array<string>): void {
    this.roleUpdates$.next(roles);
  }

  removeAllRolesFromUser(): void {
    const roleUpdate: any[] = this.roleUpdates$.getValue();
    roleUpdate.forEach((item, index) => {
      roleUpdate.splice(index, 1);
    });
    this.roleUpdates$.next(roleUpdate);
    this.roleUpdates$.next(['anonymous']);
  }

  public isAuthenticated(): boolean {
    const token = localStorage.getItem('Token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  autoRefresh(): void {
    if (!this.isAuthenticated() && this.isLogged()) {
      this.isTokenRefreshing = true;
      this.refreshToken().pipe(takeWhile(() => this.isTokenRefreshing)).subscribe(
        data => {
          localStorage.setItem('Token', data.accessToken);
          localStorage.setItem('TokenRefresh', data.refreshToken);
          localStorage.setItem('TokenExpires', data.expires);
          this.isTokenRefreshing = false;
        }
      );
    }
  }

  private getUserRoleName(): string {
    // tslint:disable-next-line:radix
    return this.getUserRole() == null ? 'anonymous' : this.convertToRoleName(parseInt(this.getUserRole()));
  }

  login(login: string, password: string): Observable<any> {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json');
    return this.http.post<any>(environment.baseUrl + '/api/Identity/login', {login, password}, {headers});
  }

  logout(): void {
    this.loadingService.load();
    this.clearUserData();
    this.loadingService.unload();
  }

  cancelToken(): Observable<any> {
    return this.http.post<any>(environment.baseUrl + '/api/Identity/cancel', {});
  }

  refreshToken(): Observable<any> {
    return this.http.post<any>(environment.baseUrl + '/api/Identity/refresh?token=' + localStorage.getItem('TokenRefresh'), {});
  }

  isLogged(): boolean {
    return localStorage.getItem('Token') !== undefined && this.isAuthenticated();
  }

  getLogin(): string {
    return localStorage.getItem('UserLogin');
  }

  getToken(): string {
    return localStorage.getItem('Token');
  }

  getUserRole() {
    return localStorage.getItem('UserRole');
  }

  getEmail(): string {
    return localStorage.getItem('UserEmail');
  }

  clearUserData(): void {
    localStorage.removeItem('Token');
    localStorage.removeItem('TokenRefresh');
    localStorage.removeItem('TokenExpires');
    localStorage.removeItem('UserLogin');
    localStorage.removeItem('UserRole');
    localStorage.removeItem('UserEmail');
    this.removeAllRolesFromUser();
  }

  convertToRoleName(roleId: number): string {
    return this.roleNames[roleId - 1];
  }
}
