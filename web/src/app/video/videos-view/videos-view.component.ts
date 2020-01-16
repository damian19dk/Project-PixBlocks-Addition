import {Component, OnInit} from '@angular/core';
import {LoadingService} from '../../services/loading.service';
import {VideoDocument} from '../../models/videoDocument.model';
import {VideoService} from '../../services/video.service';

@Component({
  selector: 'app-videos-view',
  templateUrl: './videos-view.component.html',
  styleUrls: ['./videos-view.component.css']
})
export class VideosViewComponent implements OnInit {
  videos: Array<VideoDocument>;
  error: string;

  constructor(
    private videoService: VideoService,
    private loadingService: LoadingService) {
  }

  ngOnInit() {
    this.getVideos();
  }

  getVideos() {
    this.loadingService.load();

    this.videoService.getAll(1).subscribe(
      (data: Array<VideoDocument>) => {
        this.videos = data;
        this.loadingService.unload();
      },
      error => {
        this.error = error;
        this.loadingService.unload();
      }
    );
  }
}
