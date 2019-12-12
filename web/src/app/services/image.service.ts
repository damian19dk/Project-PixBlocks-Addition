import {Injectable} from '@angular/core';
import {environment} from 'src/environments/environment';
import {HttpClient, HttpHeaders} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  constructor(private http: HttpClient) {
  }

  getImage(id: string) {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json');

    return this.http.get<any>(environment.baseUrl + '/api/Image/' + id, {headers});
  }
}
