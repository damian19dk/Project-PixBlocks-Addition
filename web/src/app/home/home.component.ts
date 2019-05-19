import { Component, OnInit } from '@angular/core';
import { VideoService } from '../services/video.service';
import { AuthenticationService } from '../services/authentication.service';
import { LoadingService } from '../services/loading.service';
import { Video } from '../models/video.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  videos: any;

  constructor(
    private videoService: VideoService,
    private loadingService: LoadingService,
    private authenticationService: AuthenticationService) { }

  ngOnInit() {
    this.videos = [
      {
        mediaId: "oWwRzyGf",
        description: "",
        pubDate: 1553868000,
        tags: "Szkola",
        image: "https://cdn.jwplayer.com/thumbs/oWwRzyGf-720.jpg",
        title: "Natural",
        feedId: null,
        sources: [
          {
            width: 0,
            height: 0,
            type: "application/vnd.apple.mpegurl",
            file: "https://cdn.jwplayer.com/manifests/oWwRzyGf.m3u8?exp=1558208400&sig=6d854bf605faa4d6535684ec078e5a23",
            label: null
          },
          {
            width: 320,
            height: 314,
            type: "video/mp4",
            file: "https://cdn.jwplayer.com/videos/oWwRzyGf-FHkZEjlj.mp4?exp=1558208400&sig=77003416dde1adc877bfc427bbbf0f17",
            label: "180p"
          },
          {
            width: 0,
            height: 0,
            type: "audio/mp4",
            file: "https://cdn.jwplayer.com/videos/oWwRzyGf-3gPNzvJj.m4a?exp=1558208400&sig=bc9b07a942c8c5113b44b5e15265af37",
            label: "AAC Audio"
          }
        ],
        tracks: [
          {
            kind: "thumbnails",
            file: "https://cdn.jwplayer.com/strips/oWwRzyGf-120.vtt"
          }
        ],
        link: "https://cdn.jwplayer.com/previews/oWwRzyGf?exp=1558208400&sig=a73d3a13e791e4ee2a8b32ad7488827d",
        duration: 220
      },
      {
        mediaId: "JEDbkfmv",
        description: "",
        pubDate: 1556710978,
        tags: null,
        image: "https://cdn.jwplayer.com/thumbs/JEDbkfmv-720.jpg",
        title: "10 second timer[Full HD,1920x1080]",
        feedId: null,
        sources: [
          {
            width: 0,
            height: 0,
            type: "application/vnd.apple.mpegurl",
            file: "https://cdn.jwplayer.com/manifests/JEDbkfmv.m3u8?exp=1558208400&sig=c68b7fe72a1cf21fb6875f91575a715a",
            label: null
          },
          {
            width: 320,
            height: 180,
            type: "video/mp4",
            file: "https://cdn.jwplayer.com/videos/JEDbkfmv-FHkZEjlj.mp4?exp=1558208400&sig=ffc76731259b733f34653e916b65bc7b",
            label: "180p"
          },
          {
            width: 480,
            height: 270,
            type: "video/mp4",
            file: "https://cdn.jwplayer.com/videos/JEDbkfmv-tWDXGSqn.mp4?exp=1558208400&sig=99ed7e093260e86af13874651a9b8d6f",
            label: "270p"
          },
          {
            width: 720,
            height: 406,
            type: "video/mp4",
            file: "https://cdn.jwplayer.com/videos/JEDbkfmv-h05wB0P4.mp4?exp=1558208400&sig=f2b4b799e22c01d40fc328f56811c182",
            label: "406p"
          },
          {
            width: 1280,
            height: 720,
            type: "video/mp4",
            file: "https://cdn.jwplayer.com/videos/JEDbkfmv-GKsjnUgI.mp4?exp=1558208400&sig=a36142969ad6e555f31e3de438e9f6c1",
            label: "720p"
          },
          {
            width: 0,
            height: 0,
            type: "audio/mp4",
            file: "https://cdn.jwplayer.com/videos/JEDbkfmv-3gPNzvJj.m4a?exp=1558208400&sig=c8b523082bfcb57225c7269edc8f9a1c",
            label: "AAC Audio"
          },
          {
            width: 1920,
            height: 1080,
            type: "video/mp4",
            file: "https://cdn.jwplayer.com/videos/JEDbkfmv-oFDSK3te.mp4?exp=1558208400&sig=fd0288302321a72a78d873a3308a8aa6",
            label: "1080p"
          }
        ],
        tracks: [
          {
            kind: "thumbnails",
            file: "https://cdn.jwplayer.com/strips/JEDbkfmv-120.vtt"
          }
        ],
        link: "https://cdn.jwplayer.com/previews/JEDbkfmv?exp=1558208400&sig=6e8a421f11f14d11f94ffdf932831aaf",
        duration: 14
      },
      {
        mediaId: "qD53ZJxa",
        description: "",
        pubDate: 1556706368,
        tags: "Prezes",
        image: "https://cdn.jwplayer.com/thumbs/qD53ZJxa-720.jpg",
        title: "Beauity",
        feedId: null,
        sources: [
          {
            width: 0,
            height: 0,
            type: "application/vnd.apple.mpegurl",
            file: "https://cdn.jwplayer.com/manifests/qD53ZJxa.m3u8?exp=1558207920&sig=ac0815fe437eb5a4389efe93cb6b2fec",
            label: null
          },
          {
            width: 320,
            height: 180,
            type: "video/mp4",
            file: "https://cdn.jwplayer.com/videos/qD53ZJxa-FHkZEjlj.mp4?exp=1558207920&sig=5f43ba921b5b027b410342b3f7bd76cb",
            label: "180p"
          },
          {
            width: 480,
            height: 270,
            type: "video/mp4",
            file: "https://cdn.jwplayer.com/videos/qD53ZJxa-tWDXGSqn.mp4?exp=1558207920&sig=33155a7ae8d301903d26ead4617f8f6f",
            label: "270p"
          },
          {
            width: 0,
            height: 0,
            type: "audio/mp4",
            file: "https://cdn.jwplayer.com/videos/qD53ZJxa-3gPNzvJj.m4a?exp=1558207920&sig=7db633f0177a20b850b64b17445d91fe",
            label: "AAC Audio"
          }
        ],
        tracks: [
          {
            kind: "thumbnails",
            file: "https://cdn.jwplayer.com/strips/qD53ZJxa-120.vtt"
          }
        ],
        link: "https://cdn.jwplayer.com/previews/qD53ZJxa?exp=1558207920&sig=4c6f48915aea9789879a34afe324fe04",
        duration: 18
      }
    ];
    
    this.getVideos();
  }

  isUserLogged() {
    return this.authenticationService.isUserLogged();
  }

  getUserLogin() {
    return this.authenticationService.getUserLogin();
  }

  getVideos() {
    this.loadingService.load();

    this.loadingService.unload();
  }

}
