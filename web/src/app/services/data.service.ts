import {HttpClient, HttpHeaders} from '@angular/common/http';
import {environment} from 'src/environments/environment';
import {retryWhen} from 'rxjs/operators';

export class DataService {

  constructor(protected BASE_PATH: string, protected http: HttpClient) {
  }

  findByTitle(title: string) {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json');

    return this.http.get<any>(environment.baseUrl + `/api/${this.BASE_PATH}/title?title=` + title, {headers}).pipe(
      retryWhen(errors => {
        return errors;
      })
    );
  }

  getOne(id: string) {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Accept-Language', localStorage.getItem('Accept-Language'));


    return this.http.get<any>(environment.baseUrl + `/api/${this.BASE_PATH}/?id=` + id, {headers}).pipe(
      retryWhen(errors => {
        return errors;
      })
    );
  }

  getAll(page: number, count: number = 10) {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Accept-Language', localStorage.getItem('Accept-Language'));

    return this.http.get<any>(environment.baseUrl + `/api/${this.BASE_PATH}/all?page=` + page + '&count=' + count, {headers}).pipe(
      retryWhen(errors => {
        return errors;
      })
    );
  }

  add(dto: any) {
    const headers = new HttpHeaders();
    return this.http.post<any>(environment.baseUrl + `/api/${this.BASE_PATH}/create`, dto, {headers}).pipe(
      retryWhen(errors => {
        return errors;
      })
    );
  }

  update(dto: any) {
    const headers = new HttpHeaders();
    return this.http.put<any>(environment.baseUrl + `/api/${this.BASE_PATH}/change`, dto, {headers}).pipe(
      retryWhen(errors => {
        return errors;
      })
    );
  }

  remove(id: string) {
    return this.http.delete<any>(environment.baseUrl + `/api/${this.BASE_PATH}?Id=` + id).pipe(
      retryWhen(errors => {
        return errors;
      })
    );
  }

  count() {
    return this.http.get<any>(environment.baseUrl + `/api/${this.BASE_PATH}/count`).pipe(
      retryWhen(errors => {
        return errors;
      })
    );
  }
}
