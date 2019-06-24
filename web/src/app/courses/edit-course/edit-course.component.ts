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

  courses: CourseDocument[];
  error: string;

  constructor(
    private courseService: CourseService,
    private loadingService: LoadingService) { }

  ngOnInit() {
    this.getCourses();
  }

  getCourses() {
    this.loadingService.load();

    this.courseService.getAllPaging(this.page).subscribe(
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

}
