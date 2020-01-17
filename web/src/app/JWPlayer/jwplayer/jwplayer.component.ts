import {Component, Input, OnChanges, OnInit} from '@angular/core';
import {HostedVideoDocument} from '../../models/hostedVideoDocument.model';
import {LoadingService} from '../../services/loading.service';
import {VideoService} from '../../services/video.service';

declare var jwplayer: any;

@Component({
  selector: 'app-jwplayer',
  templateUrl: './jwplayer.component.html',
  styleUrls: ['./jwplayer.component.css']
})
export class JWPlayerComponent implements OnInit, OnChanges {
  @Input() mediaId: string = null;
  @Input() width = 640;
  @Input() height = 480;
  hostedVideo: HostedVideoDocument;
  player: any;

  constructor(private loadingService: LoadingService,
              private videoService: VideoService) {
  }

  ngOnInit() {

  }

  ngOnChanges() {
    this.initForHostedVideo();
  }

  initForHostedVideo() {
    this.loadingService.load();
    this.videoService.getHostedVideo(this.mediaId).subscribe(
      (data: HostedVideoDocument) => {
        this.hostedVideo = data;
        setTimeout(() => {
          this.player = jwplayer('video-field').setup({
            title: this.hostedVideo.title,
            sources: this.hostedVideo.sources,
            mediaId: this.hostedVideo.mediaId,
            image: this.hostedVideo.image,
            width: this.width,
            height: this.height,
            autostart: 'viewable',
            aspectratio: '16:9',
            primary: 'html5'
          });
        }, 50);
        this.loadingService.unload();
      },
      error => {
        this.loadingService.unload();
      });
  }
}
