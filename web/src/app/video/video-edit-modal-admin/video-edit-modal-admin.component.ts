import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {VideoDocument} from '../../models/videoDocument.model';
import {FormBuilder, Validators} from '@angular/forms';
import {VideoService} from '../../services/video.service';
import {LoadingService} from '../../services/loading.service';
import {TagService} from '../../services/tag.service';
import {LanguageService} from '../../services/language.service';
import {NgbModal, NgbModalConfig} from '@ng-bootstrap/ng-bootstrap';
import {VideoDto} from '../../models/videoDto.model';
import {FormModal} from '../../models/formModal';
import {CourseService} from '../../services/course.service';
import {HostedVideoDocument} from '../../models/hostedVideoDocument.model';

@Component({
  selector: 'app-video-edit-modal-admin',
  templateUrl: './video-edit-modal-admin.component.html',
  styleUrls: ['./video-edit-modal-admin.component.css']
})
export class VideoEditModalAdminComponent extends FormModal implements OnInit {

  @Input() video: VideoDocument;
  @Output() videoChanged: EventEmitter<any> = new EventEmitter<any>();
  hostedVideo: HostedVideoDocument;

  constructor(private formBuilder: FormBuilder,
              private videoService: VideoService,
              private courseService: CourseService,
              private loadingService: LoadingService,
              private tagService: TagService,
              private languageService: LanguageService,
              protected modalService: NgbModal,
              protected modalConfig: NgbModalConfig) {
    super(modalService, modalConfig);
  }


  ngOnInit() {
    this.initFormModal();
  }

  initFormModal() {
    this.getHostedVideo();
    this.getTags(this.tagService);
    this.tagsSettings = this.tagService.getTagSettingsForMultiselect();
    this.languages = this.languageService.getAllLanguages();

    this.dataDto = new VideoDto();

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
          this.emitVideoChangedEvent();
        },
        error => {
          this.sent = true;
          this.error = error;
          this.loading = false;
        });
  }

  remove() {
    this.courseService.removeVideo(this.video.parentId, this.video.id).subscribe(
      data => {
        this.error = null;
        this.emitVideoChangedEvent();
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

  emitVideoChangedEvent() {
    this.videoChanged.emit(null);
  }

  handleFileInput(files: FileList) {
    this.form.controls.video.setValue(files.item(0));
  }

  handleImageInput(files: FileList) {
    this.form.controls.image.setValue(files.item(0));
  }

  imitateFileInput() {
    document.getElementById(this.video.mediaId).click();
  }

  imitateImageInput() {
    document.getElementById(this.video.id).click();
  }

  getDuration($event: any) {
    this.form.controls.duration.setValue($event);
  }
}
