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

  page = 1;
  count: number;

  videos: Array<VideoDocument>;
  error: string;

  constructor(
    private videoService: VideoService,
    private loadingService: LoadingService) {
  }

  ngOnInit() {
    this.getVideos();
    this.getCount();
  }

  getVideos() {
    this.loadingService.load();

    this.videoService.getAll(this.page).subscribe(
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

  getCount() {
    return this.videoService.count().subscribe(
      data => {
        this.count = parseInt(data);
      },
      error => {

      }
    );
  }
}
