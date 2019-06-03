import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { CourseDto } from '../models/courseDto.model';

@Injectable({
  providedIn: 'root'
})
export class CourseService {

  constructor(private http: HttpClient) {}

  findCourseByTitle(title: string) {
    let headers = new HttpHeaders()
    .set("Content-Type", "application/json");

    return this.http.get<any>(environment.baseUrl + "/api/Course/title?title=" + title, { headers });
  }

  getCourse(id: string) {
    let headers = new HttpHeaders()
    .set("Content-Type", "application/json");

    return this.http.get<any>(environment.baseUrl + "/api/Course/?id=" + id, { headers });
  }

  getCourses() {
    let headers = new HttpHeaders()
    .set("Content-Type", "application/json");

    return this.http.get<any>(environment.baseUrl + "/api/Course/all", { headers });
  }

  getCoursesPaging(page: number, count: number = 3) {
    let headers = new HttpHeaders()
    .set("Content-Type", "application/json");

    return this.http.get<any>(environment.baseUrl + "/api/Course/allPaging?page=" + page + "&count=" + count, { headers });
  }

  addCourse(courseDto: any) {
    let headers = new HttpHeaders()
    return this.http.post<any>(environment.baseUrl + "/api/Course/create", courseDto, { headers });
  }
}
