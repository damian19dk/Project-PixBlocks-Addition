import { Component, OnInit } from '@angular/core';
import { VideoService } from '../services/video.service';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  videos: any;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private videoService: VideoService,
    private authenticationService: AuthenticationService) { }

  ngOnInit() {
    //this.videos = this.videoService.getVideos().subscribe();
    this.videos = [
      {
        mediaId: '0',
        description: 'Ta łyżka nie istnieje.',
        pubDate: '123123',
        tags: 'Brzydko',
        image: './../../assets/img/matrix.jpg',
        title: 'Matrix',
        feedId: '0',
        sources: [
          {
            width: 300,
            height: 300,
            type: 'Kino akcji',
            file: '',
            label: 'Matrix'
          }
        ],
        tracks: [
          {
            kind: 'Kino akcji',
            file: ''
          }
        ],
        link: '',
        duration: 120
      },
      {
        mediaId: '1',
        description: 'Trzeba ich było w worki i do lasu, póki czas...',
        pubDate: '123123',
        tags: 'Brzydko',
        image: './../../assets/img/soprano.jpg',
        title: 'Rodzina Soprano',
        feedId: '1',
        sources: [
          {
            width: 300,
            height: 300,
            type: 'Kino gangsterskie',
            file: '',
            label: 'Rodzina Soprano'
          }
        ],
        tracks: [
          {
            kind: 'Kino gangsterskie',
            file: ''
          }
        ],
        link: '',
        duration: 55
      },
      {
        mediaId: '2',
        description: 'Jak taki zaczyna od kapowania, to na czym skończy? Na powązkach...',
        pubDate: '123123',
        tags: 'Brzydko',
        image: './../../assets/img/psy.jpg',
        title: 'Psy',
        feedId: '2',
        sources: [
          {
            width: 300,
            height: 300,
            type: 'Kino akcji',
            file: '',
            label: 'Psy'
          }
        ],
        tracks: [
          {
            kind: 'Kino akcji',
            file: ''
          }
        ],
        link: '',
        duration: 112
      }

    ];


  }

}
