import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {DataService} from './data.service';
import {environment} from 'src/environments/environment';
import {Observable} from 'rxjs';
import {retry} from 'rxjs/operators';

class CoursesOrder {
  courses: Array<string>;

  constructor(courses: Array<string>) {
    this.courses = courses;
  }
}

@Injectable({
  providedIn: 'root'
})
export class CourseService extends DataService {

  constructor(protected http: HttpClient) {
    super('Course', http);
  }

  getCourse(mediaId: string) {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Accept-Language', localStorage.getItem('Accept-Language'));

    return this.http.get<any>(`${environment.baseUrl}/api/${this.BASE_PATH}/${mediaId}`, {headers});
  }

  removeVideo(courseId: string, videoId: string) {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Accept-Language', localStorage.getItem('Accept-Language'));

    return this.http.delete<any>(`${environment.baseUrl}/api/${this.BASE_PATH}/video?courseId=${courseId}&videoId=${videoId}`, {headers})
      .pipe(retry(environment.maxRetryValue));
  }

  changeOrder(coursesIds: Array<string>): Observable<any> {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Accept-Language', localStorage.getItem('Accept-Language'));

    const courses = new CoursesOrder(coursesIds);
    return this.http.post<any>(environment.baseUrl + '/api/Order/courses', courses, {headers}).pipe(
      retry(environment.maxRetryValue));
  }

  addToUserHistory(courseId: string): Observable<any> {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Accept-Language', localStorage.getItem('Accept-Language'));
    return this.http.post<any>(environment.baseUrl + '/api/History/addCourseToHistory?courseId=' + courseId, {}, {headers}).pipe(
      retry(environment.maxRetryValue)
    );
  }

  getUserHistory(): Observable<any> {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Accept-Language', localStorage.getItem('Accept-Language'));
    return this.http.get<any>(environment.baseUrl + '/api/History/getUserHistory', {headers}).pipe(
      retry(environment.maxRetryValue)
    );
  }
}
