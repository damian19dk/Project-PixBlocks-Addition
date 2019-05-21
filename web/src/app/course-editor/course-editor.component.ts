import { Component, OnInit } from '@angular/core';
import { CourseService } from '../services/course.service';
import { Course } from '../models/course.model';
import { LoadingService } from '../services/loading.service';

@Component({
  selector: 'app-course-editor',
  templateUrl: './course-editor.component.html',
  styleUrls: ['./course-editor.component.css']
})
export class CourseEditorComponent implements OnInit {

  course: Course;
  error: string;

  constructor(
    private courseService: CourseService,
    private loadingService: LoadingService) { }

  ngOnInit() {
    this.getCourse();
  }

  getCourse() {
    this.loadingService.load();
    this.courseService.getCourse("Sto twarzy grzybiarzy")
      .subscribe(
        (data: Course) => {
          this.course = data;
          this.loadingService.unload();
        },
        error => {
          this.error = error;
          this.loadingService.unload();
        }
      );
  }

}
