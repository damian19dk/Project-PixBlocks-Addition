import { Component, Input, OnInit } from '@angular/core';
import { debounceTime, switchMap } from 'rxjs/operators';
import { CourseDocument } from './../../models/courseDocument.model';
import { Observable } from 'rxjs';
import { VideoService } from 'src/app/services/video.service';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.css']
})
export class SearchBarComponent implements OnInit {

  @Input() searchPhrase: string;

  constructor(private videoService: VideoService) { }

  ngOnInit() {
    
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
