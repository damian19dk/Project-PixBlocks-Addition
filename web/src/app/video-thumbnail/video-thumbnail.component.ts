import { Component, OnInit, Input } from '@angular/core';
import { Video } from './../models/video.model';

@Component({
  selector: 'app-video-thumbnail',
  templateUrl: './video-thumbnail.component.html',
  styleUrls: ['./video-thumbnail.component.css']
})
export class VideoThumbnailComponent implements OnInit {

  @Input() private video: Video;

  constructor() { }

  ngOnInit() {
    
  }

}
