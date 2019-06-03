import { Component, OnInit } from '@angular/core';
import { Video } from '../../models/videoJWPlayer.model';
import { VideoService } from './../../services/video.service';
import { ActivatedRoute } from '@angular/router';
import { LoadingService } from './../../services/loading.service'

@Component({
  selector: 'app-show-video',
  templateUrl: './show-video.component.html',
  styleUrls: ['./show-video.component.css']
})
export class ShowVideoComponent implements OnInit {
  isCollapsed1 = true;
  isCollapsed2 = false;
  isCollapsed3 = true;
  video: Video;
  error: string;

  constructor(
    private route: ActivatedRoute,
    private videoService: VideoService,
    private loadingService: LoadingService) { }

  ngOnInit() {
    this.getVideo();
  }

  getVideo() {
    this.loadingService.load();

    const id = this.route.snapshot.paramMap.get('id');
    this.videoService.getHostedVideo(id).subscribe(
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
