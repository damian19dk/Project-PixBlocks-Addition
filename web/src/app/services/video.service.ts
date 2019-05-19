import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class VideoService {
  private origin = environment.baseUrl;
  private headers = environment.headers;

  constructor(private http: HttpClient) { }

  getVideos() {
    return this.http.get<any>(this.origin + "/api/Identity/login");
  }
}
