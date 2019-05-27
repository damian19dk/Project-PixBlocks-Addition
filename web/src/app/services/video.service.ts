import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Video } from '../models/video.model';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class VideoService {
  private origin = environment.baseUrl;

  constructor(private http: HttpClient) { }

  getVideos() {
    return this.http.get<any>(this.origin + "/api/Identity/login");
  }

  getVideo(id: string):Observable<Video> {
    return this.http.get<Video>(this.origin + "/api/JWPlayer/video?id=" + id);
  }
}
