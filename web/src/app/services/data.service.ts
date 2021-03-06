import {HttpClient, HttpHeaders} from '@angular/common/http';
import {environment} from 'src/environments/environment';
import {retry} from 'rxjs/operators';

export class DataService {

  constructor(protected BASE_PATH: string, protected http: HttpClient) {
  }

  findByTitle(title: string) {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Accept-Language', localStorage.getItem('Accept-Language'));

    return this.http.get<any>(`${environment.baseUrl}/api/${this.BASE_PATH}/title?title=${title}`, {headers}).pipe(
      retry(environment.maxRetryValue)
    );
  }

  getOne(id: string) {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Accept-Language', localStorage.getItem('Accept-Language'));

    return this.http.get<any>(`${environment.baseUrl}/api/${this.BASE_PATH}/?id=${id}`, {headers}).pipe(
      retry(environment.maxRetryValue)
    );
  }

  getAll(page: number, count: number = 100) {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Accept-Language', localStorage.getItem('Accept-Language'));

    return this.http.get<any>(`${environment.baseUrl}/api/${this.BASE_PATH}/all?page=${page}&count=${count}`, {headers}).pipe(
      retry(environment.maxRetryValue)
    );
  }

  add(dto: any) {
    const headers = new HttpHeaders()
      .set('Accept-Language', localStorage.getItem('Accept-Language'));
    return this.http.post<any>(`${environment.baseUrl}/api/${this.BASE_PATH}/create`, dto, {headers}).pipe(
      retry(environment.maxRetryValue)
    );
  }

  update(dto: any) {
    const headers = new HttpHeaders()
      .set('Accept-Language', localStorage.getItem('Accept-Language'));
    return this.http.put<any>(`${environment.baseUrl}/api/${this.BASE_PATH}/change`, dto, {headers}).pipe(
      retry(environment.maxRetryValue)
    );
  }

  remove(id: string) {
    const headers = new HttpHeaders()
      .set('Accept-Language', localStorage.getItem('Accept-Language'));
    return this.http.delete<any>(`${environment.baseUrl}/api/${this.BASE_PATH}?Id=${id}`, {headers}).pipe(
      retry(environment.maxRetryValue)
    );
  }

  count() {
    const headers = new HttpHeaders()
      .set('Accept-Language', localStorage.getItem('Accept-Language'));
    return this.http.get<any>(`${environment.baseUrl}/api/${this.BASE_PATH}/count`, {headers}).pipe(
      retry(environment.maxRetryValue)
    );
  }
}
