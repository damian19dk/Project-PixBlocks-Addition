import {Component, OnInit} from '@angular/core';
import {CourseDocument} from '../../models/courseDocument.model';
import {CourseService} from '../../services/course.service';
import {LoadingService} from '../../services/loading.service';

@Component({
  selector: 'app-courses-edit',
  templateUrl: './courses-edit.component.html',
  styleUrls: ['./courses-edit.component.css']
})
export class CoursesEditComponent implements OnInit {

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
    this.getCount();
  }

  getCourses() {
    this.loadingService.load();

    this.courseService.getAll(this.page).subscribe(
      (data: Array<CourseDocument>) => {
        this.courses = data;
        this.loadingService.unload();
      },
      error => {
        this.error = error;
        this.loadingService.unload();
      }
    );
  }

  getCount() {
    return this.courseService.count().subscribe(
      data => {
        this.count = parseInt(data);
      },
      error => {

      }
    );
  }
}

