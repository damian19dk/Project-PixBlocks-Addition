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
        // tslint:disable-next-line:radix
        this.count = parseInt(data);
      }
    );
  }
}
