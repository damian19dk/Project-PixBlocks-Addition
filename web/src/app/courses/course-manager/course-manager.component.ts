import { Component, OnInit } from '@angular/core';
import { CourseService } from '../../services/course.service';
import { CourseDocument } from '../../models/courseDocument.model';
import { LoadingService } from '../../services/loading.service';

@Component({
  selector: 'app-course-manager',
  templateUrl: './course-manager.component.html',
  styleUrls: ['./course-manager.component.css']
})
export class CourseManagerComponent implements OnInit {

  course: CourseDocument;
  error: string;
  isNewCourseSelected: boolean;

  constructor(
    private courseService: CourseService,
    private loadingService: LoadingService) { }

  ngOnInit() {
    this.isNewCourseSelected = true;
  }

  getCourse() {
    this.loadingService.load();
    this.courseService.getOne("Sto twarzy grzybiarzy")
      .subscribe(
        (data: CourseDocument) => {
          this.course = data;
          this.loadingService.unload();
        },
        error => {
          this.error = error;
          this.loadingService.unload();
        }
      );
  }

  changeToEditCourse() {
    this.isNewCourseSelected = false;
  }

  changeToNewCourse() {
    this.isNewCourseSelected = true;
  }

}
