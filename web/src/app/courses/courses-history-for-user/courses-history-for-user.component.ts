import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {CourseDocument} from '../../models/courseDocument.model';
import {CourseService} from '../../services/course.service';
import {LoadingService} from '../../services/loading.service';

@Component({
  selector: 'app-courses-history-for-user',
  templateUrl: './courses-history-for-user.component.html',
  styleUrls: ['./courses-history-for-user.component.css']
})
export class CoursesHistoryForUserComponent implements OnInit {

  courses: Array<CourseDocument>;
  error: string;
  @Output() coursesLoaded: EventEmitter<any> = new EventEmitter<any>();

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
        this.emitCoursesLoadedEvent();
        this.courses = data.slice(0, 1);
        this.loadingService.unload();
      },
      error => {
        this.error = error;
        this.loadingService.unload();
      }
    );
  }

  emitCoursesLoadedEvent() {
    this.coursesLoaded.emit(this.courses.length);
  }
}
