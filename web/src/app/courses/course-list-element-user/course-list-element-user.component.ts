import {Component, Input, OnInit} from '@angular/core';
import {CourseDocument} from '../../models/courseDocument.model';

@Component({
  selector: 'app-course-list-element-user',
  templateUrl: './course-list-element-user.component.html',
  styleUrls: ['./course-list-element-user.component.css']
})
export class CourseListElementUserComponent implements OnInit {

  @Input() course: CourseDocument;

  constructor() {
  }

  ngOnInit() {
  }

}
