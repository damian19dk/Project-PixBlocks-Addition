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
  progress: number;

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
    this.getCourses();

    this.route.params.subscribe(
      () => {
        this.quiz = null;
        this.progress = 0;

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
    this.loadingService.load();

    this.courseService.getProgress(this.courseDocument.id).subscribe(
      (data: number) => {
        this.progress = data;
        this.loadingService.unload();
      },
      error => {
        this.progress = 0;
        this.loadingService.unload();
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
    this.courseService.addToUserHistory(this.courseDocument.id).subscribe(
      data => {

      },
      error => {

      }
    );
  }
}
