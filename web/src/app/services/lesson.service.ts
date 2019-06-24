import { Injectable } from '@angular/core';
import { DataService } from './data.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class LessonService extends DataService {

  constructor(protected http: HttpClient) {
    super("Lesson", http);
  }

}
