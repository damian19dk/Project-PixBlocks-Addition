import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {VideoService} from '../../services/video.service';
import {VideoDocument} from '../../models/videoDocument.model';

@Component({
  selector: 'app-video-choose-admin',
  templateUrl: './video-choose-admin.component.html',
  styleUrls: ['./video-choose-admin.component.css']
})
export class VideoChooseAdminComponent implements OnInit {

  @Input() buttonName: string;
  @Output() videoChanged: EventEmitter<any> = new EventEmitter<any>();
  videos: Array<VideoDocument>;
  selectedVideo: VideoDocument;

  constructor(private videoService: VideoService) {
  }

  ngOnInit() {
    this.getVideos();
  }

  getVideos() {
    this.videoService.getAll(1).subscribe(
      (data: Array<VideoDocument>) => {
        this.videos = data;
      },
      error => {

      }
    );
  }

  save() {
    this.refreshOtherThumbnails();
  }

  chooseVideo(video: VideoDocument) {
    this.selectedVideo = video;
  }

  refreshOtherThumbnails() {
    this.videoChanged.emit(null);
  }
}
