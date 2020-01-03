import {Component, OnInit} from '@angular/core';
import {HostedVideoDocument} from '../../models/hostedVideoDocument.model';
import {VideoService} from '../../services/video.service';
import {ActivatedRoute} from '@angular/router';
import {LoadingService} from '../../services/loading.service';
import {VideoDocument} from 'src/app/models/videoDocument.model';
import {TagService} from 'src/app/services/tag.service';
import {CourseDocument} from 'src/app/models/courseDocument.model';
import {CourseService} from 'src/app/services/course.service';

@Component({
  selector: 'app-show-video',
  templateUrl: './show-video.component.html',
  styleUrls: ['./show-video.component.css']
})

export class ShowVideoComponent implements OnInit {
  courseDocument: CourseDocument;
  video: HostedVideoDocument = null;
  videoDocument: VideoDocument = null;
  error: string;
  videos: Array<VideoDocument>;
  courses: Array<CourseDocument>;
  page = 1;

  constructor(
    private route: ActivatedRoute,
    private videoService: VideoService,
    private loadingService: LoadingService,
    private courseService: CourseService,
    private tagService: TagService) {
  }

  ngOnInit() {
    this.getCourses();

    this.route.params.subscribe(
      () => {
        this.getCourse();
        this.getVideo();
        this.getHostedVideo();
      }
    );
  }

  getHostedVideo() {
    this.loadingService.load();

    const id = this.route.snapshot.paramMap.get('mediaId');
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

    const mediaId = this.route.snapshot.paramMap.get('mediaId');
    this.videoService.getVideo(mediaId).subscribe(
      (data: VideoDocument) => {
        this.videoDocument = data[0]
        this.videoDocument.tags = this.tagService.toTagsList(this.videoDocument.tags);
        this.loadingService.unload();
        console.log(this.videoDocument);
      },
      error => {
        this.error = error;
        this.loadingService.unload();
      });
  }

  getCourses() {
    this.loadingService.load();

    this.courseService.getAll(this.page).subscribe(
      (data: Array<CourseDocument>) => {
        this.courses = data;
        this.loadingService.unload();
      },
      error => {
        this.error = error;
        this.loadingService.unload();
      }
    );
  }

  getCourse() {
    this.loadingService.load();
    const courseId = this.route.snapshot.paramMap.get('id');

    this.courseService.getOne(courseId).subscribe(
      (data: CourseDocument) => {
        this.courseDocument = data;
        this.loadingService.unload();
      },
      error => {
        this.courseDocument = null;
        this.loadingService.unload();
      }
    );
  }

}
