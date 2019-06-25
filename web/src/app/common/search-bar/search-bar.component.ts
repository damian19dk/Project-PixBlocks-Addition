import { Component, Input, OnInit } from '@angular/core';
import { debounceTime, switchMap } from 'rxjs/operators';
import { CourseDocument } from './../../models/courseDocument.model';
import { Observable } from 'rxjs';
import { VideoService } from 'src/app/services/video.service';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.css']
})
export class SearchBarComponent implements OnInit {

  searchVideoForm: FormGroup;

  constructor(private formBuilder: FormBuilder,
    private videoService: VideoService) { }

  ngOnInit() {
    this.searchVideoForm = this.formBuilder.group({
      searchPhrase: [null]
    });
  }

  searchVideo() {
    
  }

  search= (text$: Observable<string>) => {
    return text$.pipe(
      debounceTime(300),
      switchMap((searchText) => this.videoService.findByTitle(searchText))
    );
  }

  formatter = (x: CourseDocument) =>
  x.title;

}
