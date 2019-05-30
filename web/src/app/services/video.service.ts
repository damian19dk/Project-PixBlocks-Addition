import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Video } from '../models/video.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VideoService {

  constructor(private http: HttpClient) { }

  getVideos() {
    let headers = new HttpHeaders()
    .set("Access-Control-Allow-Origin", environment.baseUrl)
    .set("Authorization", "Bearer " + localStorage.getItem("Token"))
    .set("Content-Type", "application/json");

    return this.http.get<any>(environment.baseUrl + "/api/Identity/login", { headers });
  }

  getVideo(id: string):Observable<Video> {
    let headers = new HttpHeaders()
    .set("Access-Control-Allow-Origin", environment.baseUrl)
    .set("Authorization", "Bearer " + localStorage.getItem("Token"))
    .set("Content-Type", "application/json");
    
    return this.http.get<Video>(environment.baseUrl + "/api/JWPlayer/video?id=" + id, { headers });
  }
}
