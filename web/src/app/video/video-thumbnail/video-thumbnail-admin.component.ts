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

@Component({
  selector: 'app-video-thumbnail',
  templateUrl: './video-thumbnail-admin.component.html',
  styleUrls: ['./video-thumbnail-admin.component.css']
})
export class VideoThumbnailAdminComponent extends FormModal implements OnInit {

  @Input() video: VideoDocument;
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
      video: [null]
    });
  }

  edit() {
    this.submitted = true;

    if (this.form.invalid) {
      return;
    }

    this.loading = true;

    this.dataDto.from(this.form);
    this.dataDto.image = this.fileToUpload;
    const tags = this.form.value.tags;
    this.dataDto.tags = this.tagService.toTagsString(tags);
    const formData = this.dataDto.toFormData();

    this.videoService.update(formData)
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

  remove() {
    this.videoService.remove(this.video.id).subscribe(
      () => {
        this.refreshOtherThumbnails();
      },
      error => {
        this.error = error;
      }
    );
  }

  refreshOtherThumbnails() {
    this.videoChanged.emit(null);
  }

  handleFileInput(files: FileList) {
    this.fileToUpload = files.item(0);
    this.fileUploadMessage = this.fileToUpload.size > 0 ? 'Gotowy do wysłania' : 'Wybierz plik';
  }

  imitateFileInput() {
    document.getElementById('video').click();
  }

  imitateImageInput() {
    document.getElementById('image').click();
  }
}
