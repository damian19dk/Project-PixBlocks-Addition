import { Component, OnInit } from '@angular/core';
import { VideoService } from '../services/video.service';
import { AuthService } from '../services/auth.service';
import { LoadingService } from '../services/loading.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  videos: any;
  error: string;

  constructor(
    private videoService: VideoService,
    private loadingService: LoadingService,
    private authenticationService: AuthService) { }

  ngOnInit() {
    if (this.authenticationService.isLogged()) {
      this.getVideos();
    }
  }

  isLogged() {
    return this.authenticationService.isLogged();
  }

  getLogin() {
    return this.authenticationService.getLogin();
  }

  getVideos() {
    this.loadingService.load();

    this.videoService.getHostedPlaylist("15GkO0Bz").subscribe(
      data => {
        this.videos = data.playlist;
        this.loadingService.unload();
      },
      error => {
        this.error = error;
        this.loadingService.unload();
      }
    );
  }

}
