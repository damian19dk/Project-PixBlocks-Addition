import {Component, EventEmitter, Input, OnChanges, Output, SimpleChanges} from '@angular/core';

@Component({
  selector: 'app-video-preview',
  templateUrl: './video-preview.component.html',
  styleUrls: ['./video-preview.component.css']
})
export class VideoPreviewComponent implements OnChanges {

  @Input() video: File;
  @Output() videoLoaded: EventEmitter<number> = new EventEmitter<number>();

  constructor() {
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (this.video !== null) {
      const URL = window.URL;
      const videoNode = document.querySelector('video');
      videoNode.src = URL.createObjectURL(this.video);

      const i = setInterval(() => {
        if (videoNode.readyState > 0) {
          this.emitVideoLoadedEvent(Math.round(videoNode.duration));
          clearInterval(i);
        }
      }, 100);
    }
  }

  emitVideoLoadedEvent(videoDuration: number) {
    this.videoLoaded.emit(videoDuration);
  }
}
