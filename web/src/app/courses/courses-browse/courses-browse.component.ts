import {Component, Input, OnInit} from '@angular/core';
import {CourseDocument} from 'src/app/models/courseDocument.model';

@Component({
  selector: 'app-courses-browse',
  templateUrl: './courses-browse.component.html',
  styleUrls: ['./courses-browse.component.css']
})
export class CoursesBrowseComponent implements OnInit {

  @Input() course: CourseDocument;

  constructor() {
  }

  ngOnInit() {
  }

}
