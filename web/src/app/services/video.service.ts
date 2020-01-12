import {Injectable} from '@angular/core';
import {environment} from 'src/environments/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {HostedVideoDocument} from '../models/hostedVideoDocument.model';
import {Observable} from 'rxjs';
import {DataService} from './data.service';
import {retry} from 'rxjs/operators';

class VideosOrder {
  videos: Array<string>;

  constructor(videos: Array<string>) {
    this.videos = videos;
  }
}

@Injectable({
  providedIn: 'root'
})
export class VideoService extends DataService {

  constructor(protected http: HttpClient) {
    super('Video', http);
  }

  getVideo(mediaId: string) {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json');

    return this.http.get<any>(environment.baseUrl + '/api/Video/' + mediaId, {headers});
  }

  getHostedPlaylist(id: string) {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json');

    return this.http.get<any>(environment.baseUrl + '/api/JWPlayer/playlist?id=' + id, {headers});
  }

  getHostedVideo(id: string): Observable<HostedVideoDocument> {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json');

    return this.http.get<HostedVideoDocument>(environment.baseUrl + '/api/JWPlayer/video?id=' + id, {headers});
  }

  changeOrder(videosIds: Array<string>): Observable<any> {
    const headers = new HttpHeaders();
    const videos = new VideosOrder(videosIds);
    return this.http.post<any>(environment.baseUrl + '/api/Order/videos', videos, {headers}).pipe(
      retry(environment.maxRetryValue));
  }
}