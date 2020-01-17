import {Component, Input, OnChanges, SimpleChanges} from '@angular/core';

@Component({
  selector: 'app-video-preview',
  templateUrl: './video-preview.component.html',
  styleUrls: ['./video-preview.component.css']
})
export class VideoPreviewComponent implements OnChanges {

  @Input() video: File;

  constructor() {
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (this.video !== null) {
      const URL = window.URL;
      document.querySelector('video').src = URL.createObjectURL(this.video);
    }
  }
}
