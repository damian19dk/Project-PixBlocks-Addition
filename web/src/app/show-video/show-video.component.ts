import { Component, OnInit, Pipe, PipeTransform } from '@angular/core';
import { Video } from '../models/video.model';
import { VideoService } from '../services/video.service';

@Component({
  selector: 'app-show-video',
  templateUrl: './show-video.component.html',
  styleUrls: ['./show-video.component.css']
})
export class ShowVideoComponent implements OnInit {

  private video: Video;

  constructor(private videoService: VideoService) { }

  ngOnInit() {
    this.video = new Video();
    this.getVideo();
  }

  getVideo() {
    this.videoService.getVideo("oWwRzyGf").subscribe(
      (data: Video) => this.video = {
        mediaId: data.mediaId,
        description: data.description,
        pubDate: data.pubDate,
        tags: data.tags,
        image: data.image,
        title: data.title,
        feedId: data.feedId,
        sources: data.sources,
        tracks: data.tracks,
        link: data.link,
        duration: data.duration
      },
      error => {
        console.log("Błąd ładowania danych!");
       });


  }

}
