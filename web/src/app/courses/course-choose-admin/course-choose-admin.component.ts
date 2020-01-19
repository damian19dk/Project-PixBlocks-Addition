import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {CourseService} from '../../services/course.service';
import {CourseDocument} from '../../models/courseDocument.model';

@Component({
  selector: 'app-course-choose-admin',
  templateUrl: './course-choose-admin.component.html',
  styleUrls: ['./course-choose-admin.component.css']
})
export class CourseChooseAdminComponent implements OnInit {

  @Input() buttonName: string;
  @Input() course: CourseDocument = null;
  @Output() courseSelected: EventEmitter<any> = new EventEmitter<any>();
  courses: Array<CourseDocument>;
  selectedCourse: CourseDocument;
  isSelected = false;

  constructor(private courseService: CourseService) {
  }

  ngOnInit() {
    if (this.course !== undefined && this.course !== null) {
      this.isSelected = true;
      this.selectedCourse = this.course;
    }
    this.getCourses();
  }

  getCourses() {
    this.courseService.getAll(1).subscribe(
      (data: Array<CourseDocument>) => {
        this.courses = data;
      }
    );
  }

  chooseCourse(course: CourseDocument) {
    this.isSelected = true;
    this.selectedCourse = course;
    this.emitCourseSelectedEvent();
  }

  emitCourseSelectedEvent() {
    this.courseSelected.emit(this.selectedCourse);
  }
}
