import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { CourseDto } from '../models/courseDto.model';

@Injectable({
  providedIn: 'root'
})
export class CourseService {

  private origin: string;

  constructor(
    private http: HttpClient) {
      this.origin = environment.baseUrl;
     }

  getCourse(id: string) {
    return this.http.get<any>(this.origin + "/api/Course/title?title=" + id);
  }

  getCourses() {
    return this.http.get<any>(this.origin + "/api/Course/all");
  }

  addCourse(courseDto: CourseDto) {
    let headers = environment.headers;

    return this.http.post<CourseDto>(this.origin + "/api/Course/create", courseDto, { headers });
  }
}
