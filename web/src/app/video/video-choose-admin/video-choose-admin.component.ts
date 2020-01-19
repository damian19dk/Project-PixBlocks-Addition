import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {VideoDocument} from '../../models/videoDocument.model';
import {VideoService} from "../../services/video.service";

@Component({
  selector: 'app-video-choose-admin',
  templateUrl: './video-choose-admin.component.html',
  styleUrls: ['./video-choose-admin.component.css']
})
export class VideoChooseAdminComponent implements OnInit {

  @Input() buttonName: string;
  @Input() video: VideoDocument = null;
  @Output() videoSelected: EventEmitter<any> = new EventEmitter<any>();
  videos: Array<VideoDocument>;
  selectedVideo: VideoDocument;
  isSelected = false;

  constructor(private videoService: VideoService) {
  }

  ngOnInit() {
    if (this.video !== undefined && this.video !== null) {
      this.isSelected = true;
      this.selectedVideo = this.video;
    }
    this.getVideos();
  }

  getVideos() {
    this.videoService.getAll(1).subscribe(
      (data: Array<VideoDocument>) => {
        this.videos = data;
      }
    );
  }

  chooseVideo(video: VideoDocument) {
    this.isSelected = true;
    this.selectedVideo = video;
    this.emitVideoSelectedEvent();
  }

  emitVideoSelectedEvent() {
    this.videoSelected.emit(this.selectedVideo);
  }
}
