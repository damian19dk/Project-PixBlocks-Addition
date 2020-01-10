import { Quiz } from './../video/models/quiz';
import { Observable } from 'rxjs/internal/Observable';
import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { environment } from 'src/environments/environment';

@Injectable({providedIn: 'root'})
export class QuizService {
  constructor(private http: HttpClient) {}

  Get(quizId: string): Observable<Quiz> {
    return this.http.get(`${environment.baseUrl}/api/Quiz/${quizId}`);
  }

  Create()

  function Update() {

  }

}
