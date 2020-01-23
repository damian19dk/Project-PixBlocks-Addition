import {Component, OnInit} from '@angular/core';
import {CourseService} from '../../services/course.service';
import {LoadingService} from '../../services/loading.service';
import {CourseDocument} from '../../models/courseDocument.model';

@Component({
  selector: 'app-courses-view-list-user',
  templateUrl: './courses-view-list-user.component.html',
  styleUrls: ['./courses-view-list-user.component.css']
})
export class CoursesViewListUserComponent implements OnInit {

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
