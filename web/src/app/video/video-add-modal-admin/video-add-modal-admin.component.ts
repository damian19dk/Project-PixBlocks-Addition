import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormBuilder, Validators} from '@angular/forms';
import {VideoService} from '../../services/video.service';
import {LoadingService} from '../../services/loading.service';
import {TagService} from '../../services/tag.service';
import {LanguageService} from '../../services/language.service';
import {NgbModal, NgbModalConfig} from '@ng-bootstrap/ng-bootstrap';
import {VideoDto} from '../../models/videoDto.model';
import {FormModal} from '../../models/formModal';
import {CourseDocument} from '../../models/courseDocument.model';

@Component({
  selector: 'app-video-add-modal-admin',
  templateUrl: './video-add-modal-admin.component.html',
  styleUrls: ['./video-add-modal-admin.component.css']
})
export class VideoAddModalAdminComponent extends FormModal implements OnInit {

  @Input() course: CourseDocument;
  @Output() videoChanged: EventEmitter<any> = new EventEmitter<any>();
  image: any;

  constructor(private formBuilder: FormBuilder,
              private videoService: VideoService,
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
    this.getTags(this.tagService);
    this.tagsSettings = this.tagService.getTagSettingsForMultiselect();
    this.languages = this.languageService.getAllLanguages();

    this.dataDto = new VideoDto();

    this.sent = false;
    this.submitted = false;
    this.loading = false;
    this.error = null;

    this.form = this.formBuilder.group({
      parentId: [this.course.id, Validators.required],
      mediaId: [null],
      id: [null],
      title: [null, Validators.required],
      description: [null, [Validators.required, Validators.minLength(3), Validators.maxLength(10000)]],
      premium: [this.course.premium],
      tags: [null],
      language: [this.course.language],
      pictureUrl: [null],
      image: [null],
      video: [null]
    });
  }

  create() {
    this.submitted = true;

    if (this.form.invalid) {
      return;
    }

    this.loading = true;

    this.dataDto.from(this.form);
    this.dataDto.image = this.image;
    this.dataDto.video = this.fileToUpload;
    const tags = this.form.value.tags;
    this.dataDto.tags = this.tagService.toTagsString(tags);
    const formData = this.dataDto.toFormData();

    this.videoService.add(formData)
      .subscribe(
        () => {
          this.sent = true;
          this.error = null;
          this.loading = false;
          this.refreshOtherThumbnails();
        },
        error => {
          this.sent = true;
          this.error = error;
          this.loading = false;
        });
  }

  refreshOtherThumbnails() {
    this.videoChanged.emit(null);
  }

  handleFileInput(files: FileList) {
    this.fileToUpload = files.item(0);
  }

  handleImageInput(files: FileList) {
    this.image = files.item(0);
  }

  imitateFileInput() {
    document.getElementById('newVideo').click();
  }

  imitateImageInput() {
    document.getElementById('newVideoImage').click();
  }
}
