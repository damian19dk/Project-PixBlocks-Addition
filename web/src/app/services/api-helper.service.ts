import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { RequestOptions, Headers } from '@angular/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ApiHelperService {
  private loginUrl = 'localhost:8080/api/Identity/login';

  constructor(private http: HttpClient) {}



  get(url) {
    return this.http.get<any>(this.loginUrl + url, {
      headers: this.generateHeaders()
    });
  }

  post(url, data, contentType = 'application/json; charset=utf-8') {
    return this.http.post<any>(this.loginUrl + url, data, {
      headers: this.generateHeaders()
    });
  }

  postFormData(url, data) {
    return this.http.post<any>(this.loginUrl + url, this.objectToFormData(data), {
      headers: this.generateFormDataHeaders()
    });
  }

  toData<T extends Array<any>>(reqResponse: { response: string; data: T }): T {
    return reqResponse.data;
  }

  private generateHeaders(): HttpHeaders {
    return new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8',
      Accept: 'application/json',
      Authorization: 'Bearer ' + localStorage.getItem('token') || ''
    });
  }

  private generateFormDataHeaders(): HttpHeaders {
    return new HttpHeaders({
      Accept: 'application/json',
      Authorization: 'Bearer ' + localStorage.getItem('token') || ''
    });
  }

  private objectToFormData(obj, form?, namespace?) {
    const fd = form || new FormData();
    var formKey;

    for (var property in obj) {
      if (obj.hasOwnProperty(property) && obj[property] !== undefined) {
        if (namespace) {
          formKey = namespace + '[' + property + ']';
        } else {
          formKey = property;

          if (
            typeof obj[property] === 'object' &&
            !(obj[property] instanceof File)
          ) {
            this.objectToFormData(obj[property], fd, property);
          } else {
            fd.append(formKey, obj[property]);
          }
        }
      }
    }

    return fd;
  }
}
