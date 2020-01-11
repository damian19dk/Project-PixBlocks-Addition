import {Component, OnInit} from '@angular/core';

@Component({
  selector: 'app-course-manager',
  templateUrl: './course-manager-admin.component.html',
  styleUrls: ['./course-manager-admin.component.css']
})
export class CourseManagerAdminComponent implements OnInit {

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
