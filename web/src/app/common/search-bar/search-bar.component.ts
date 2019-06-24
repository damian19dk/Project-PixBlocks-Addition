import { Component, Input, OnInit } from '@angular/core';
import { debounceTime, switchMap } from 'rxjs/operators';
import { CourseService } from 'src/app/services/course.service';
import { CourseDocument } from './../../models/courseDocument.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.css']
})
export class SearchBarComponent implements OnInit {

  @Input() searchPhrase: string;

  constructor(private courseService: CourseService) { }

  ngOnInit() {
    
  }

  search= (text$: Observable<string>) => {
    return text$.pipe(
      debounceTime(300),
      switchMap((searchText) => this.courseService.findByTitle(searchText))
    );
  }

  formatter = (x: CourseDocument) =>
  x.title;

}
