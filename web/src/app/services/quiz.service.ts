import { UpdateQuizPayload } from "./../models/quiz.model";
import { DataService } from "./data.service";
import { Observable } from "rxjs/internal/Observable";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { CreateQuizPayload } from "../models/quiz.model";
import { environment } from "src/environments/environment";

@Injectable({ providedIn: "root" })
export class QuizService extends DataService {
  constructor(protected http: HttpClient) {
    super("Quiz", http);
  }

  createQuiz(payload: CreateQuizPayload): Observable<any> {
    return this.http.post(`${environment.baseUrl}/Quiz/create`, payload);
  }

  updateQuiz(payload: UpdateQuizPayload): Observable<any> {
    return this.http.put(`${environment.baseUrl}/Quiz/update`, payload);
  }
}