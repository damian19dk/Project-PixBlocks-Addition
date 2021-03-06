import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {VideoDocument} from '../../models/videoDocument.model';
import {FormBuilder, Validators} from '@angular/forms';
import {LoadingService} from '../../services/loading.service';
import {TagService} from '../../services/tag.service';
import {LanguageService} from '../../services/language.service';
import {NgbModal, NgbModalConfig} from '@ng-bootstrap/ng-bootstrap';
import {FormModal} from '../../models/formModal';
import {VideoService} from '../../services/video.service';
import {VideoDto} from '../../models/videoDto.model';
import {HostedVideoDocument} from '../../models/hostedVideoDocument.model';
import {CourseDocument} from '../../models/courseDocument.model';
import {CourseService} from '../../services/course.service';

@Component({
  selector: 'app-video-thumbnail',
  templateUrl: './video-thumbnail-admin.component.html',
  styleUrls: ['./video-thumbnail-admin.component.css']
})
export class VideoThumbnailAdminComponent extends FormModal implements OnInit {

  @Input() video: VideoDocument;
  @Output() videoChanged: EventEmitter<any> = new EventEmitter<any>();
  hostedVideo: HostedVideoDocument;
  course: CourseDocument = null;

  constructor(private formBuilder: FormBuilder,
              private videoService: VideoService,
              private loadingService: LoadingService,
              private tagService: TagService,
              private languageService: LanguageService,
              protected modalService: NgbModal,
              protected modalConfig: NgbModalConfig,
              private courseService: CourseService) {
    super(modalService, modalConfig);
  }

  ngOnInit() {
    this.initFormModal();
  }

  initFormModal() {
    this.getCourse();
    this.getHostedVideo();
    this.getTags(this.tagService);
    this.tagsSettings = this.tagService.getTagSettingsForMultiselect();
    this.languages = this.languageService.getAllLanguages();

    this.dataDto = new VideoDto();

    this.sent = false;
    this.submitted = false;
    this.loading = false;
    this.error = null;

    this.form = this.formBuilder.group({
      parentId: [this.video.parentId, Validators.required],
      mediaId: [null],
      id: [this.video.id],
      title: [this.video.title, Validators.required],
      description: [this.video.description, [Validators.required, Validators.minLength(3), Validators.maxLength(10000)]],
      premium: [this.video.premium],
      tags: [this.tagService.toTagsList(this.video.tags)],
      language: [this.video.language],
      pictureUrl: [this.video.picture],
      image: [null],
      video: [null],
      duration: [null]
    });
  }

  edit() {
    this.submitted = true;

    if (this.form.invalid) {
      return;
    }

    this.loading = true;

    this.dataDto.from(this.form);
    const formData = this.dataDto.toFormData();

    this.videoService.update(formData)
      .subscribe(
        () => {
          this.sent = true;
          this.error = null;
          this.loading = false;
          this.refreshOtherVideos();
        },
        error => {
          this.sent = true;
          this.error = error;
          this.loading = false;
        });
  }

  remove() {
    this.videoService.remove(this.video.id).subscribe(
      () => {
        this.error = null;
        this.refreshOtherVideos();
      },
      error => {
        this.error = error;
      }
    );
  }

  async getHostedVideo() {
    this.videoService.getHostedVideo(this.video.mediaId).subscribe(
      (data: HostedVideoDocument) => {
        this.hostedVideo = data;
        this.error = null;
      },
      error => {
        this.error = error;
      }
    );
  }

  async getCourse() {
    this.courseService.getOne(this.video.parentId).subscribe(
      (data: CourseDocument) => {
        this.course = data;
        this.error = null;
      },
      error => {
        this.error = error;
      }
    );
  }


  refreshOtherVideos() {
    this.videoChanged.emit(null);
  }

  handleVideoInput(files: FileList) {
    this.form.controls.video.setValue(files.item(0));
  }

  handleImageInput(files: FileList) {
    this.form.controls.image.setValue(files.item(0));
  }

  imitateVideoInput() {
    document.getElementById('video').click();
  }

  imitateImageInput() {
    document.getElementById('image').click();
  }

  selectCourse($event: any) {
    this.form.controls.parentId.setValue($event.id);
  }

  getDuration($event: any) {
    this.form.controls.duration.setValue($event);
  }
}
