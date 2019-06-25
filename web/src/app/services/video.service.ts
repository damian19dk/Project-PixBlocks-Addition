import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Video } from '../models/videoJWPlayer.model';
import { Observable } from 'rxjs';
import { DataService } from './data.service';

@Injectable({
  providedIn: 'root'
})
export class VideoService extends DataService {

  constructor(protected http: HttpClient) {
    super("Video", http);
  }

  getVideo(mediaId: string) {
    let headers = new HttpHeaders()
      .set("Content-Type", "application/json");

    return this.http.get<any>(environment.baseUrl + "/api/Video/mediaId=" + mediaId, { headers });
  }

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
}
