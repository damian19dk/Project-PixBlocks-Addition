import { Component, OnInit } from '@angular/core';
import { CourseService } from 'src/app/services/course.service';
import { debounceTime } from 'rxjs/operators';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.css']
})
export class SearchBarComponent implements OnInit {

  searchPhrase: string;

  constructor(private courseService: CourseService) { }

  ngOnInit() {
    this.searchPhrase = '';
  }

  search() {
    this.courseService.findCourseByTitle(this.searchPhrase).pipe(
      debounceTime(400)
      ).subscribe(
      data => {

      },
      error => {

      }
    );
  }

}
