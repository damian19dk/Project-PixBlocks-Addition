import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LessonService {

  origin: string;

  constructor(private http: HttpClient) {
    this.origin = environment.baseUrl;
   }

  getLesson(id: string) {
    return this.http.get<any>(this.origin + "/api/Lesson/title?title=" + id);
  }
}
