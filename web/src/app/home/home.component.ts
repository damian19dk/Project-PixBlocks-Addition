import { Component, OnInit } from '@angular/core';
import { VideoService } from '../services/video.service';
import { AuthService } from '../services/auth.service';
import { LoadingService } from '../services/loading.service';
import { VideoDocument } from '../models/videoDocument.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  count: number;
  page: number = 1;

  videos: VideoDocument[];
  error: string;

  constructor(
    private videoService: VideoService,
    private loadingService: LoadingService,
    private authenticationService: AuthService) { }

  ngOnInit() {
    this.getVideos();
    this.getCount();
  }

  isLogged() {
    return this.authenticationService.isLogged();
  }

  getLogin() {
    return this.authenticationService.getLogin();
  }

  getVideos() {
    this.loadingService.load();

    this.videoService.getAll(this.page).subscribe(
      data => {
        this.videos = data.filter((video) => { return video.status == "ready" });
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
