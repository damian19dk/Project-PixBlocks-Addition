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

  getHostedPlaylist(id: string) {
    let headers = new HttpHeaders()
      .set("Content-Type", "application/json");

    return this.http.get<any>(environment.baseUrl + "/api/JWPlayer/playlist?id=" + id, { headers });
  }

  getHostedVideo(id: string): Observable<Video> {
    let headers = new HttpHeaders()
      .set("Content-Type", "application/json");

    return this.http.get<Video>(environment.baseUrl + "/api/JWPlayer/video?id=" + id, { headers });
  }

  addHostedVideo() {

  }

  getVideos() {

  }

  getVideo(id: string): Observable<Video> {
    let headers = new HttpHeaders()
      .set("Content-Type", "application/json");

    return this.http.get<Video>(environment.baseUrl + "/api/Video/video?id=" + id, { headers });
  }
}
