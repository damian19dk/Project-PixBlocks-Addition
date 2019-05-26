import { Component, OnInit } from '@angular/core';
import { Video } from '../models/video.model';
import { VideoService } from '../services/video.service';
import { ActivatedRoute } from '@angular/router';
import { LoadingService } from '../services/loading.service';

@Component({
  selector: 'app-show-video',
  templateUrl: './show-video.component.html',
  styleUrls: ['./show-video.component.css']
})
export class ShowVideoComponent implements OnInit {
  public isCollapsed1 = true;
  public isCollapsed2 = false;
  public isCollapsed3 = true;
  video: Video;
  error: string;

  constructor(
    private route: ActivatedRoute,
    private videoService: VideoService,
    private loadingService: LoadingService,
  ) { }

  ngOnInit() {
    this.getVideo();
  }

  getVideo() {
    this.loadingService.load();

    const id = this.route.snapshot.paramMap.get('id');
    this.videoService.getVideo(id).subscribe(
      (data: Video) => {
        this.video = data;
        this.loadingService.unload();
      },
      error => {
        this.error = error;
        this.loadingService.unload();
      });

  }

}
