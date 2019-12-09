import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {VideoDocument} from '../../models/videoDocument.model';
import {FormBuilder, Validators} from '@angular/forms';
import {LoadingService} from '../../services/loading.service';
import {TagService} from '../../services/tag.service';
import {LanguageService} from '../../services/language.service';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {FormModal} from '../../models/formModal';
import {VideoService} from '../../services/video.service';
import {VideoDto} from '../../models/videoDto.model';

@Component({
  selector: 'app-video-thumbnail',
  templateUrl: './video-thumbnail.component.html',
  styleUrls: ['./video-thumbnail.component.css']
})
export class VideoThumbnailComponent extends FormModal implements OnInit {

  @Input() video: VideoDocument;
  @Output() editVideoComponent: EventEmitter<any> = new EventEmitter<any>();
  image: any;

  constructor(private formBuilder: FormBuilder,
              private videoService: VideoService,
              private loadingService: LoadingService,
              private tagService: TagService,
              private languageService: LanguageService,
              protected modalService: NgbModal) {
    super(modalService);
  }


  ngOnInit() {
    this.getPicture();

    this.tagsList = this.tagService.getTags();
    this.tagsSettings = this.tagService.getTagSettingsForMultiselect();
    this.languages = this.languageService.getAllLanguages();

    this.dataDto = new VideoDto();

    this.sent = false;
    this.submitted = false;
    this.loading = false;
    this.error = null;

    this.form = this.formBuilder.group({
      parentId: [null, Validators.required],
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
    this.dataDto.tags = this.tagService.toTagsString(tags === null ? null : tags.map(e => e.text));
    const formData = this.dataDto.toFormData();

    this.videoService.update(formData)
      .subscribe(
        data => {
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
      data => {
        this.refreshOtherThumbnails();
      },
      error => {
        this.error = error;
      }
    );
  }

  refreshOtherThumbnails() {
    this.editVideoComponent.emit(null);
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

  private getPicture() {
    if (this.video.picture == null) {
      this.video.picture = 'https://mdrao.ca/wp-content/uploads/2018/03/DistanceEdCourse_ResitExam.png';
      return;
    }
  }
}
