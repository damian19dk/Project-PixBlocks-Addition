import { Component, OnInit } from '@angular/core';
import { CourseDocument } from 'src/app/models/courseDocument.model';
import { CourseService } from 'src/app/services/course.service';
import { LoadingService } from 'src/app/services/loading.service';

@Component({
  selector: 'app-edit-course',
  templateUrl: './edit-course.component.html',
  styleUrls: ['./edit-course.component.css']
})
export class EditCourseComponent implements OnInit {

  page: number = 1;
  count: number;

  courses: CourseDocument[];
  error: string;

  constructor(
    private courseService: CourseService,
    private loadingService: LoadingService) { }

  ngOnInit() {
    this.getCourses();
    this.getCount();
  }

  getCourses() {
    this.loadingService.load();

    this.courseService.getAll(this.page).subscribe(
      (data: CourseDocument[]) => {
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
