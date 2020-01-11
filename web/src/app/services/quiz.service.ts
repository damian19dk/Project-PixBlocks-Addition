import {CreateQuizPayload, UpdateQuizPayload} from '../models/quiz.model';
import {DataService} from './data.service';
import {Observable} from 'rxjs/internal/Observable';
import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {environment} from 'src/environments/environment';

@Injectable({providedIn: 'root'})
export class QuizService extends DataService {
  constructor(protected http: HttpClient) {
    super('Quiz', http);
  }

  createQuiz(payload: CreateQuizPayload): Observable<any> {
    return this.http.post(`${environment.baseUrl}/api/${this.BASE_PATH}/create`, payload);
  }

  updateQuiz(payload: UpdateQuizPayload): Observable<any> {
    return this.http.put(`${environment.baseUrl}/api/${this.BASE_PATH}/update`, payload);
  }
}
