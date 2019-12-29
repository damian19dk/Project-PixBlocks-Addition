import {Component, Input, OnInit} from '@angular/core';
import {CourseDocument} from 'src/app/models/courseDocument.model';

@Component({
  selector: 'app-course-thumbnail-user',
  templateUrl: './course-thumbnail-user.component.html',
  styleUrls: ['./course-thumbnail-user.component.css']
})
export class CourseThumbnailUserComponent implements OnInit {

  @Input() course: CourseDocument;

  constructor() {
  }

  ngOnInit() {
  }

}
