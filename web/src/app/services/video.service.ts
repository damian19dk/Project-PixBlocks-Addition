import {Injectable} from '@angular/core';
import {environment} from 'src/environments/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {HostedVideoDocument} from '../models/hostedVideoDocument.model';
import {Observable} from 'rxjs';
import {DataService} from './data.service';
import {retry} from 'rxjs/operators';

class VideosOrder {
  courseId: string;
  videos: Array<string>;

  constructor(courseId: string, videos: Array<string>) {
    this.courseId = courseId;
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

    return this.http.get<any>(`${environment.baseUrl}/api/${this.BASE_PATH}/${mediaId}`, {headers}).pipe(
      retry(environment.maxRetryValue)
    );
  }

  getHostedPlaylist(id: string) {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json');

    return this.http.get<any>(`${environment.baseUrl}/api/JWPlayer/playlist?id=${id}`, {headers}).pipe(
      retry(environment.maxRetryValue)
    );
  }

  getHostedVideo(mediaId: string): Observable<HostedVideoDocument> {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json');

    return this.http.get<HostedVideoDocument>(`${environment.baseUrl}/api/JWPlayer/video?id=${mediaId}`, {headers}).pipe(
      retry(environment.maxRetryValue)
    );
  }

  changeOrder(courseId: string, videosIds: Array<string>): Observable<any> {
    const headers = new HttpHeaders();
    const videos = new VideosOrder(courseId, videosIds);
    return this.http.post<any>(environment.baseUrl + '/api/Order/videos', videos, {headers}).pipe(
      retry(environment.maxRetryValue));
  }

  setVideoHistory(videoId: string, time: number): Observable<any> {
    const headers = new HttpHeaders();
    return this.http.post<any>(`${environment.baseUrl}/api/VideoHistory/set?videoId=${videoId}&time=${time}`, {}, {headers}).pipe(
      retry(environment.maxRetryValue));
  }

  getProgress(id: string) {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Accept-Language', localStorage.getItem('Accept-Language'));
    return this.http.get<any>(`${environment.baseUrl}/api/VideoHistory/progressVideo/${id}`, {headers}).pipe(
      retry(environment.maxRetryValue)
    );
  }
}
