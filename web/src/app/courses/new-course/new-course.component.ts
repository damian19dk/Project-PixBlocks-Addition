import { Component, OnInit } from '@angular/core';
import { CourseService } from 'src/app/services/course.service';
import { LoadingService } from 'src/app/services/loading.service';

@Component({
  selector: 'app-new-course',
  templateUrl: './new-course.component.html',
  styleUrls: ['./new-course.component.css']
})
export class NewCourseComponent implements OnInit {

  constructor(private courseService: CourseService,
    private loadingService: LoadingService) { }

  ngOnInit() {
  }

}
