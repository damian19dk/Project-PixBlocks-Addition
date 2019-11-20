import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {TagService} from 'src/app/services/tag.service';
import {VideoService} from 'src/app/services/video.service';
import {VideoDto} from 'src/app/models/videoDto.model';
import {debounceTime, filter, switchMap} from 'rxjs/operators';
import {Observable} from 'rxjs';
import {CourseService} from '../../services/course.service';
import {CourseDocument} from '../../models/courseDocument.model';

@Component({
  selector: 'app-add-video',
  templateUrl: './add-video.component.html',
  styleUrls: ['./add-video.component.css']
})
export class AddVideoComponent implements OnInit {

  form: FormGroup;
  loading: boolean;
  submitted: boolean;
  sent: boolean;
  videoDto: VideoDto;
  error: string;

  tagsList = [];
  tagsSettings = {};

  fileToUpload: File = null;
  fileUploadMessage = 'Wybierz plik';

  constructor(private formBuilder: FormBuilder,
              private videoService: VideoService,
              private tagService: TagService,
              private courseService: CourseService) {
  }

  ngOnInit() {
    this.tagsList = this.tagService.getTags();
    this.tagsSettings = this.tagService.getTagSettingsForMultiselect();

    this.videoDto = new VideoDto();

    this.sent = false;
    this.submitted = false;
    this.loading = false;
    this.error = null;

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
      video: [null]
    });
  }


  get f() {
    return this.form.controls;
  }

  create() {
    this.submitted = true;

    if (this.form.invalid) {
      return;
    }

    this.loading = true;

    this.videoDto.of(this.form);
    this.videoDto.parentId = this.form.value.parentId.id;
    this.videoDto.video = this.fileToUpload;

    const formData = this.videoDto.toFormData();

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

  searchCourse = (text$: Observable<string>) => {
    return text$.pipe(
      debounceTime(300),
      filter(text => text !== ''),
      switchMap((searchText) => this.courseService.findByTitle(searchText))
    );
  }

  formatter = (x: CourseDocument) =>
    x.title


  handleFileInput(files: FileList) {
    this.fileToUpload = files.item(0);
    this.fileUploadMessage = this.fileToUpload.size > 0 ? 'Gotowy do wysłania' : 'Wybierz plik';
  }

  imitateFileInput() {
    this.fileToUpload = null;
    document.getElementById('video').click();
  }
}
