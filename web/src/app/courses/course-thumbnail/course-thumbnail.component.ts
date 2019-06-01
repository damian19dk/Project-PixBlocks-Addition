import { Component, OnInit, Input } from '@angular/core';
import { Course } from 'src/app/models/course.model';

@Component({
  selector: 'app-course-thumbnail',
  templateUrl: './course-thumbnail.component.html',
  styleUrls: ['./course-thumbnail.component.css']
})
export class CourseThumbnailComponent implements OnInit {

  @Input() course: Course;
  
  constructor() { }

  ngOnInit() {
    this.course.picture = this.getPicture();
  }

  private getPicture() {
    if(this.course.picture == null)
      return null;

    return this.course.picture.substring(this.course.picture.indexOf('image/') + 6);
  }

}
