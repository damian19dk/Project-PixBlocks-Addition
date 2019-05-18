import { Component, OnInit } from '@angular/core';
import { Video } from '../models/video.model';
import { VideoService } from '../services/video.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-show-video',
  templateUrl: './show-video.component.html',
  styleUrls: ['./show-video.component.css']
})
export class ShowVideoComponent implements OnInit {

  private video: Video;
  error: string;

  constructor(
    private route: ActivatedRoute,
    private videoService: VideoService,
  ) { }

  ngOnInit() {
    this.getVideo();
  }

  getVideo() {
    const id = this.route.snapshot.paramMap.get('id');
    
    this.videoService.getVideo(id).subscribe(
      (data: Video) => {
        this.video = data
      },
      error => {
        this.error = error;
      });

  }

}
