import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { DataService } from './data.service';
import { environment } from 'src/environments/environment';



@Injectable({
  providedIn: 'root'
})
export class CourseService extends DataService {

  constructor(protected http: HttpClient) {
    super("Course", http);
  }

  getCourse(mediaId: string) {
    let headers = new HttpHeaders()
      .set("Content-Type", "application/json");

    return this.http.get<any>(environment.baseUrl + "/api/Course/" + mediaId, { headers });
  }

}
