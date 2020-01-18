import {CreateQuizPayload, UpdateQuizPayload} from '../models/quiz.model';
import {DataService} from './data.service';
import {Observable} from 'rxjs/internal/Observable';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {environment} from 'src/environments/environment';

@Injectable({providedIn: 'root'})
export class QuizService extends DataService {
  constructor(protected http: HttpClient) {
    super('Quiz', http);
  }

  getOne(id: string): Observable<any> {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Accept-Language', localStorage.getItem('Accept-Language'));
    return this.http.get<any>(`${environment.baseUrl}/api/${this.BASE_PATH}/${id}`, {headers});
  }

  add(payload: CreateQuizPayload): Observable<any> {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Accept-Language', localStorage.getItem('Accept-Language'));
    return this.http.post<any>(`${environment.baseUrl}/api/${this.BASE_PATH}/create`, payload, {headers});
  }

  update(payload: UpdateQuizPayload): Observable<any> {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Accept-Language', localStorage.getItem('Accept-Language'));
    return this.http.put<any>(`${environment.baseUrl}/api/${this.BASE_PATH}/update`, payload, {headers});
  }
}
