import {Component, OnInit} from '@angular/core';

@Component({
  selector: 'app-video-manager',
  templateUrl: './video-manager.component.html',
  styleUrls: ['./video-manager.component.css']
})
export class VideoManagerComponent implements OnInit {
  error: string;
  isNewVideoSelected: boolean;

  constructor() {
  }

  ngOnInit() {
    this.isNewVideoSelected = true;
  }

  changeToVideosView() {
    this.isNewVideoSelected = false;
  }

  changeToNewVideo() {
    this.isNewVideoSelected = true;
  }
}
