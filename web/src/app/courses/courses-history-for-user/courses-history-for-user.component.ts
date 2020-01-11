import {Component, OnInit} from '@angular/core';
import {CourseDocument} from '../../models/courseDocument.model';
import {CourseService} from '../../services/course.service';
import {LoadingService} from '../../services/loading.service';

@Component({
  selector: 'app-courses-history-for-user',
  templateUrl: './courses-history-for-user.component.html',
  styleUrls: ['./courses-history-for-user.component.css']
})
export class CoursesHistoryForUserComponent implements OnInit {

  page = 1;
  count: number;

  courses: Array<CourseDocument>;
  error: string;

  constructor(
    private courseService: CourseService,
    private loadingService: LoadingService) {
  }

  ngOnInit() {
    this.getCourses();
  }

  getCourses() {
    this.loadingService.load();

    this.courseService.getUserHistory().subscribe(
      (data: Array<CourseDocument>) => {
        this.courses = data.slice(0, 2);
        this.loadingService.unload();
      },
      error => {
        this.error = error;
        this.loadingService.unload();
      }
    );
  }
}
