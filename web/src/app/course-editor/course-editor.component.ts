import { Component, OnInit } from '@angular/core';
import { CourseService } from '../services/course.service';
import { Course } from '../models/course.model';

@Component({
  selector: 'app-course-editor',
  templateUrl: './course-editor.component.html',
  styleUrls: ['./course-editor.component.css']
})
export class CourseEditorComponent implements OnInit {

  course: Course;
  error: string;

  constructor(
    private courseService: CourseService) { }

  ngOnInit() {
    this.getCourse();
  }

  getCourse() {
    this.courseService.getCourse("Super Kurs Javy")
    .subscribe(
      (data: Course) => {
        this.course = data;
      },
      error => {
        this.error = error;
      }
    );
  }

}
