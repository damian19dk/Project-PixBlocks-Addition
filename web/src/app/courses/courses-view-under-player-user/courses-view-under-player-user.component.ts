import {Component, OnInit} from '@angular/core';
import {CourseDocument} from '../../models/courseDocument.model';
import {CourseService} from '../../services/course.service';
import {LoadingService} from '../../services/loading.service';

@Component({
  selector: 'app-courses-view',
  templateUrl: './courses-view-under-player-user.component.html',
  styleUrls: ['./courses-view-under-player-user.component.css']
})
export class CoursesViewUnderPlayerUserComponent implements OnInit {

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

    this.courseService.getAll(1).subscribe(
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
}

