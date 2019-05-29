import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { LessonDto } from '../models/lessonDto.model';

@Injectable({
  providedIn: 'root'
})
export class LessonService {

  constructor(private http: HttpClient) {}

  getLesson(id: string) {
    let headers = new HttpHeaders()
    .set("Access-Control-Allow-Origin", environment.baseUrl)
    .set("Authorization", "Bearer " + localStorage.getItem("Token-Authorization"))
    .set("Content-Type", "application/json");

    return this.http.get<any>(environment.baseUrl + "/api/Lesson/title?title=" + id, { headers });
  }

  addLesson(lessonDto: LessonDto) {
    let headers = new HttpHeaders()
    .set("Access-Control-Allow-Origin", environment.baseUrl)
    .set("Authorization", "Bearer " + localStorage.getItem("Token-Authorization"))
    .set("Content-Type", "application/json");

    return this.http.post<LessonDto>(environment.baseUrl + "/api/Lesson/create", lessonDto, { headers });
  }
}
