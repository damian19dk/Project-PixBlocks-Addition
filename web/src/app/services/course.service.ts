import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoadingService } from './loading.service';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CourseService {

  private origin: string;

  constructor(
    private http: HttpClient,
    private loadingService: LoadingService) {
      this.origin = environment.baseUrl;
     }

  getCourses() {
    return this.http.get<any>(this.origin + "/api/Course");
  }
}