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
      .set('Content-Type', 'application/json');

    return this.http.get<any>(environment.baseUrl + '/api/Course/' + mediaId, {headers});
  }

  changeOrder(coursesIds: Array<string>): Observable<any> {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json');
    const courses = new CoursesOrder(coursesIds);
    return this.http.post<any>(environment.baseUrl + '/api/Order/courses', courses, {headers}).pipe(
      retry(10));
  }

  addToUserHistory(courseId: string): Observable<any> {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Accept-Language', localStorage.getItem('Accept-Language'));
    return this.http.post<any>(environment.baseUrl + '/api/History/addCourseToHistory?courseId=' + courseId, {}, {headers}).pipe(
      retry(10)
    );
  }

  getUserHistory(): Observable<any> {
    const headers = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('Accept-Language', localStorage.getItem('Accept-Language'));
    return this.http.get<any>(environment.baseUrl + '/api/History/getUserHistory', {headers}).pipe(
      retry(10)
    );
  }
}
