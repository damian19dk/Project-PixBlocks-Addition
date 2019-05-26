import { Component, OnInit } from '@angular/core';
import { Video } from 'src/app/models/video.model';
import { VideoService } from 'src/app/services/video.service';
import { ActivatedRoute } from '@angular/router';
import { LoadingService } from 'src/app/services/loading.service';

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.css']
})
export class OverviewComponent implements OnInit {
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
