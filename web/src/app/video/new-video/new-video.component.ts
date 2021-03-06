import {Component, OnInit} from '@angular/core';
import {FormBuilder, Validators} from '@angular/forms';
import {TagService} from 'src/app/services/tag.service';
import {VideoService} from 'src/app/services/video.service';
import {VideoDto} from 'src/app/models/videoDto.model';
import {LanguageService} from '../../services/language.service';
import {Form} from '../../models/form';

@Component({
  selector: 'app-new-video',
  templateUrl: './new-video.component.html',
  styleUrls: ['./new-video.component.css']
})
export class NewVideoComponent extends Form implements OnInit {
  constructor(private formBuilder: FormBuilder,
              private videoService: VideoService,
              private tagService: TagService,
              private languageService: LanguageService) {
    super();
  }

  ngOnInit() {
    this.getTags(this.tagService);
    this.tagsSettings = this.tagService.getTagSettingsForMultiselect();
    this.languages = this.languageService.getAllLanguages();

    this.dataDto = new VideoDto();

    this.form = this.formBuilder.group({
      parentId: [null, Validators.required],
      mediaId: [null],
      title: [null, Validators.required],
      description: [null, [Validators.required, Validators.minLength(3), Validators.maxLength(10000)]],
      premium: [false],
      tags: [null],
      language: ['pl'],
      pictureUrl: [null],
      image: [null],
      video: [null, Validators.required],
      duration: [0, Validators.required]
    });
  }

  create() {
    this.submitted = true;

    if (this.form.invalid) {
      return;
    }

    this.loading = true;

    this.dataDto.from(this.form);
    const formData = this.dataDto.toFormData();

    this.videoService.add(formData)
      .subscribe(
        data => {
          this.sent = true;
          this.error = null;
          this.loading = false;
        },
        error => {
          this.sent = true;
          this.error = error;
          this.loading = false;
        });
  }

  handleImageInput(files: FileList) {
    this.form.controls.image.setValue(files.item(0));
  }

  handleFileInput(files: FileList) {
    this.form.controls.video.setValue(files.item(0));
  }

  imitateImageInput() {
    document.getElementById('image').click();
  }

  imitateFileInput() {
    document.getElementById('video').click();
  }

  selectCourse($event: any) {
    this.form.controls.parentId.setValue($event.id);
  }

  getDuration($event: any) {
    this.form.controls.duration.setValue($event);
  }
}
