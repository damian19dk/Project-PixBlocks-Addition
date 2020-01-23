import {Component, OnInit} from '@angular/core';
import {HostedVideoDocument} from '../../models/hostedVideoDocument.model';
import {VideoService} from '../../services/video.service';
import {ActivatedRoute} from '@angular/router';
import {LoadingService} from '../../services/loading.service';
import {VideoDocument} from 'src/app/models/videoDocument.model';
import {TagService} from 'src/app/services/tag.service';
import {CourseDocument} from 'src/app/models/courseDocument.model';
import {CourseService} from 'src/app/services/course.service';
import {AuthService} from '../../services/auth.service';
import {QuizService} from '../../services/quiz.service';
import {Quiz} from '../../models/quiz.model';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import * as $ from 'jquery';
declare  var jQuery: any;

declare var jwplayer: any;

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
  quiz: Quiz = null;
  player: any;
  courseProgress = 0;

  isCourseAddedToHistory = false;

  constructor(
    private route: ActivatedRoute,
    private videoService: VideoService,
    private loadingService: LoadingService,
    private courseService: CourseService,
    private tagService: TagService,
    private authService: AuthService,
    private quizService: QuizService) {
  }

  ngOnInit() {
    // $('.multi-item-carousel').carousel({
    //   interval: false
    // });

    // // for every slide in carousel, copy the next slide's item in the slide.
    // // Do the same for the next, next item.
    // $('.multi-item-carousel .item').each(function(){
    //   let next = $(this).next();
    //   if (!next.length) {
    //     next = $(this).siblings(':first');
    //   }
    //   next.children(':first-child').clone().appendTo($(this));

    //   if (next.next().length > 0) {
    //     next.next().children(':first-child').clone().appendTo($(this));
    //   } else {
    //     $(this).siblings(':first').children(':first-child').clone().appendTo($(this));
    //   }
    // });
    this.getCourses();

    this.route.params.subscribe(
      () => {
        this.quiz = null;
        this.courseProgress = 0;

        this.getCourse();
        this.getVideo();
        this.getHostedVideo();
      }
    );
  }


  getHostedVideo() {
    this.loadingService.load();
    window.scrollTo({top: 0, behavior: 'smooth'});

    const id = this.route.snapshot.paramMap.get('mediaId');
    this.videoService.getHostedVideo(id).subscribe(
      (data: HostedVideoDocument) => {
        this.video = data;
        this.error = null;

        setTimeout(() => {
          this.player = jwplayer('video-field').setup({
            title: this.video.title,
            sources: this.video.sources,
            mediaId: this.video.mediaId,
            image: this.video.image,
            autostart: 'viewable',
            aspectratio: '16:9',
            primary: 'html5'
          });
          jwplayer('video-field').on('play', () => {
            this.addCourseToUserHistory();
          });
          jwplayer('video-field').on('pause', () => {
            this.setVideoHistory();
          });
          jwplayer('video-field').on('complete', () => {
            this.setVideoHistory();
          });
        }, 50);

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
        this.videoDocument = data[0];
        this.videoDocument.tags = this.tagService.toTagsList(this.videoDocument.tags);
        this.error = null;
        if (this.videoDocument.quizId !== null && this.videoDocument.quizId !== undefined) {
          this.getQuiz();
        }
        this.loadingService.unload();
      },
      error => {
        this.error = error;
        this.loadingService.unload();
      });
  }

  async getCourses() {
    this.loadingService.load();

    this.courseService.getAll(1).subscribe(
      (data: Array<CourseDocument>) => {
        this.courses = data;
        this.error = null;
        this.loadingService.unload();
      },
      error => {
        this.error = error;
        this.loadingService.unload();
      }
    );
  }

  async getCourse() {
    this.loadingService.load();
    const courseId = this.route.snapshot.paramMap.get('id');

    this.courseService.getOne(courseId).subscribe(
      (data: CourseDocument) => {
        this.courseDocument = data;
        this.error = null;
        this.getProgress();
        this.getVideosProgresses();
        this.loadingService.unload();
      },
      error => {
        this.courseDocument = null;
        this.loadingService.unload();
      }
    );
  }

  async getQuiz() {
    this.loadingService.load();

    this.quizService.getOne(this.videoDocument.quizId).subscribe(
      (data: Quiz) => {
        this.quiz = data;
        this.loadingService.unload();
      },
      error => {
        this.quiz = null;
        this.loadingService.unload();
      }
    );
  }

  async getProgress() {
    this.courseService.getProgress(this.courseDocument.id).subscribe(
      (data: number) => {
        this.courseProgress = data;
      },
      error => {
        this.courseProgress = 0;
      }
    );
  }

  async getVideosProgresses() {
    this.courseDocument.courseVideos.forEach(video =>
      this.videoService.getProgress(video.id).subscribe(
        data => {
          video.progress = (data === null)
            ? 0
            : (data.time / video.duration) * 100;
        },
        error => {
        }
      ));
  }

  async setVideoHistory() {
    // tslint:disable-next-line:radix
    const currentPosition = parseInt(jwplayer('video-field').getPosition());
    this.videoService.setVideoHistory(this.videoDocument.id, currentPosition).subscribe(
      data => {
        this.getProgress();
      },
      error => {
      }
    );
  }

  getTag(name: string) {
    return this.tagService.getTagDto(name);
  }

  isPremiumUser(): boolean {
    return this.authService.isPremium();
  }

  forPremium(premium: boolean): boolean {
    return premium ? this.authService.isPremium() : true;
  }

  addCourseToUserHistory() {
    if (!this.isCourseAddedToHistory) {
      this.courseService.addToUserHistory(this.courseDocument.id).subscribe(
        data => {
        },
        error => {
        });
      this.isCourseAddedToHistory = true;
    }
  }
}
