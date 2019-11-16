import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms';
import {Observable} from 'rxjs';
import {debounceTime, filter, switchMap} from 'rxjs/operators';
import {CourseDocument} from '../../models/courseDocument.model';
import {CourseService} from '../../services/course.service';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.css']
})
export class SearchBarComponent implements OnInit {

  form: FormGroup;

  constructor(private formBuilder: FormBuilder,
              private courseService: CourseService) {
  }

  ngOnInit() {
    this.form = this.formBuilder.group({
      searchPhrase: [null]
    });
  }

  search = (text$: Observable<string>) => {
    return text$.pipe(
      debounceTime(300),
      filter(text => text !== ''),
      switchMap((searchText) => this.courseService.findByTitle(searchText))
    );
  }

  formatter = (x: CourseDocument) => x.title;

}
