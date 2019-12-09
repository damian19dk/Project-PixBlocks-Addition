import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DataService } from './data.service';

@Injectable({
  providedIn: 'root'
})
export class CourseService extends DataService {

  constructor(protected http: HttpClient) {
    super('Course', http);
  }
}
