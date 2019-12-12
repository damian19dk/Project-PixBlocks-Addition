import {Component, OnInit} from '@angular/core';
import {FormBuilder, Validators} from '@angular/forms';
import {TagService} from 'src/app/services/tag.service';
import {VideoService} from 'src/app/services/video.service';
import {VideoDto} from 'src/app/models/videoDto.model';
import {debounceTime, filter, switchMap} from 'rxjs/operators';
import {Observable} from 'rxjs';
import {CourseService} from '../../services/course.service';
import {CourseDocument} from '../../models/courseDocument.model';
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
              private languageServce: LanguageService,
              private courseService: CourseService) {
    super();
  }

  ngOnInit() {
    this.tagsList = this.tagService.getTags();
    this.tagsSettings = this.tagService.getTagSettingsForMultiselect();
    this.languages = this.languageServce.getAllLanguages();

    this.dataDto = new VideoDto();

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

  create() {
    this.submitted = true;

    if (this.form.invalid) {
      return;
    }

    this.loading = true;

    this.dataDto.from(this.form);
    this.dataDto.parentId = this.form.value.parentId.id;
    this.dataDto.video = this.fileToUpload;
    const tags = this.form.value.tags;
    this.dataDto.tags = this.tagService.toTagsString(tags === null ? null : tags.map(e => e.text));
    const formData = this.dataDto.toFormData();
    console.log(this.dataDto);

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
