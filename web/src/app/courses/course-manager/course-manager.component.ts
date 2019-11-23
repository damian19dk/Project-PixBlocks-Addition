import {Component, OnInit} from '@angular/core';

@Component({
  selector: 'app-course-manager',
  templateUrl: './course-manager.component.html',
  styleUrls: ['./course-manager.component.css']
})
export class CourseManagerComponent implements OnInit {

  error: string;
  isNewCourseSelected: boolean;

  constructor() {
  }

  ngOnInit() {
    this.isNewCourseSelected = true;
  }

  changeToCoursesView() {
    this.isNewCourseSelected = false;
  }

  changeToNewCourse() {
    this.isNewCourseSelected = true;
  }
}
