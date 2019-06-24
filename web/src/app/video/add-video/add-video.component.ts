import { Component, OnInit } from '@angular/core';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { TagService } from 'src/app/services/tag.service';
import { VideoService } from 'src/app/services/video.service';
import { VideoDto } from 'src/app/models/videoDto.model';

@Component({
  selector: 'app-add-video',
  templateUrl: './add-video.component.html',
  styleUrls: ['./add-video.component.css']
})
export class AddVideoComponent implements OnInit {

  newVideoForm: FormGroup;
  loading: boolean;
  submitted: boolean;
  sent: boolean;
  returnUrl: string;
  videoDto: VideoDto;
  error: string;

  tagsList = [];
  tagsSettings = {};

  fileToUpload: File = null;
  fileUploadMessage: string = 'Wybierz plik';

  constructor(private formBuilder: FormBuilder,
    private videoService: VideoService,
    private tagService: TagService) { }

  ngOnInit() {
    this.tagsList = this.tagService.getTags();
    this.tagsSettings = this.tagService.getTagSettingsForMultiselect();

    this.videoDto = new VideoDto();

    this.sent = false;
    this.submitted = false;
    this.loading = false;
    this.error = null;

    this.newVideoForm = this.formBuilder.group({
      parentId: [null],
      mediaId: [null],
      title: [null, Validators.required],
      description: [null, [Validators.required, Validators.minLength(3), Validators.maxLength(10000)]],
      premium: [false],
      tags: [null],
      language: ['Polski'],
      pictureUrl: [null],
      image: [null],
      video: [null]
    });
  }


  get f() { return this.newVideoForm.controls; }

  createNewVideo() {
    this.submitted = true;

    if (this.newVideoForm.invalid) {
      return;
    }

    this.loading = true;

    this.videoDto = this.newVideoForm.value;
    this.videoDto.image = this.fileToUpload;
    let formData = new FormData();

    this.videoDto.parentId != null ? formData.append('parentId', this.videoDto.parentId) : null;
    this.videoDto.id != null ? formData.append('id', this.videoDto.id) : null;
    this.videoDto.premium != null ? formData.append('premium', String(this.videoDto.premium)) : null;
    this.videoDto.title != null ? formData.append('title', this.videoDto.title) : null;
    this.videoDto.description != null ? formData.append('description', this.videoDto.description) : null;
    this.videoDto.pictureUrl != null ? formData.append('pictureUrl', this.videoDto.pictureUrl) : null;
    this.videoDto.image != null ? formData.append('image', this.fileToUpload) : null;
    this.videoDto.language != null ? formData.append('language', this.videoDto.language) : null;
    this.videoDto.tags != null ? formData.append('tags', this.videoDto.tags) : null;


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

  handleFileInput(files: FileList) {
    this.fileToUpload = files.item(0);
    if(this.fileToUpload.size > 0) {
      this.fileUploadMessage = 'Gotowy do wysłania';
    }
    else {
      this.fileUploadMessage = 'Wybierz plik';
    } 
  }

  imitateFileInput() {
    document.getElementById('image').click();
  }

}
