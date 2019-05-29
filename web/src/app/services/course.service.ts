import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { CourseDto } from '../models/courseDto.model';

@Injectable({
  providedIn: 'root'
})
export class CourseService {

  constructor(private http: HttpClient) {}

  getCourse(id: string) {
    let headers = new HttpHeaders()
    .set("Access-Control-Allow-Origin", environment.baseUrl)
    .set("Authorization", "Bearer " + localStorage.getItem("Authorization"))
    .set("Content-Type", "application/json");

    return this.http.get<any>(environment.baseUrl + "/api/Course/title?title=" + id, { headers });
  }

  getCourses() {
    let headers = new HttpHeaders()
    .set("Access-Control-Allow-Origin", environment.baseUrl)
    .set("Authorization", "Bearer " + localStorage.getItem("Authorization"))
    .set("Content-Type", "application/json");

    return this.http.get<any>(environment.baseUrl + "/api/Course/all", { headers });
  }

  addCourse(courseDto: CourseDto) {
    let headers = new HttpHeaders()
    .set("Access-Control-Allow-Origin", environment.baseUrl)
    .set("Authorization", "Bearer " + localStorage.getItem("Authorization"))
    .set("Content-Type", "application/json");

    return this.http.post<CourseDto>(environment.baseUrl + "/api/Course/create", courseDto, { headers });
  }
}
