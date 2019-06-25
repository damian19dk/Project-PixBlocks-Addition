import { Component, OnInit } from '@angular/core';
import { HostedVideoDocument } from '../../models/hostedVideoDocument.model';
import { VideoService } from './../../services/video.service';
import { ActivatedRoute } from '@angular/router';
import { LoadingService } from './../../services/loading.service'
import { VideoDocument } from 'src/app/models/videoDocument.model';
import { TagService } from 'src/app/services/tag.service';

@Component({
  selector: 'app-show-video',
  templateUrl: './show-video.component.html',
  styleUrls: ['./show-video.component.css']
})
export class ShowVideoComponent implements OnInit {
  video: HostedVideoDocument = null;
  videoDocument: VideoDocument = null;
  error: string;

  constructor(
    private route: ActivatedRoute,
    private videoService: VideoService,
    private loadingService: LoadingService,
    private tagService: TagService) { }

  ngOnInit() {
    this.getVideo();
    this.getHostedVideo();
  }

  getHostedVideo() {
    this.loadingService.load();

    const id = this.route.snapshot.paramMap.get('id');
    this.videoService.getHostedVideo(id).subscribe(
      (data: HostedVideoDocument) => {
        this.video = data;
        this.loadingService.unload();
      },
      error => {
        this.error = error;
        this.loadingService.unload();
      });
  }

  getVideo() {
    this.loadingService.load();

    const mediaId = this.route.snapshot.paramMap.get('id');
    this.videoService.getVideo(mediaId).subscribe(
      (data: VideoDocument) => {
        this.videoDocument = data;
        this.videoDocument.tags = this.tagService.toTagsList(this.videoDocument.tags);
        this.loadingService.unload();
      },
      error => {
        this.error = error;
        this.loadingService.unload();
      });
  }

}
