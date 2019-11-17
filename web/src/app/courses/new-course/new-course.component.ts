import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {CourseService} from 'src/app/services/course.service';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {CourseDto} from 'src/app/models/courseDto.model';
import {TagService} from 'src/app/services/tag.service';

@Component({
  selector: 'app-new-course',
  templateUrl: './new-course.component.html',
  styleUrls: ['./new-course.component.css']
})
export class NewCourseComponent implements OnInit {

  @ViewChild('labelImport', null)
  labelImport: ElementRef;

  form: FormGroup;
  loading: boolean;
  submitted: boolean;
  sent: boolean;
  courseDto: CourseDto;
  error: string;

  tagsList = [];
  tagsSettings = {};

  fileToUpload: File = null;
  fileUploadMessage: string;

  constructor(private formBuilder: FormBuilder,
              private courseService: CourseService,
              private tagService: TagService) {
  }

  ngOnInit() {
    this.tagsList = this.tagService.getTags();
    this.tagsSettings = this.tagService.getTagSettingsForMultiselect();

    this.courseDto = new CourseDto();

    this.sent = false;
    this.submitted = false;
    this.loading = false;
    this.error = null;

    this.form = this.formBuilder.group({
      parentId: [null],
      title: [null, Validators.required],
      description: [null, [Validators.required, Validators.minLength(3), Validators.maxLength(10000)]],
      premium: [false],
      tags: [null],
      language: ['Polski'],
      pictureUrl: [null],
      image: [null]
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

    this.courseDto.of(this.form);
    this.courseDto.image = this.fileToUpload;
    this.courseDto.tags = this.tagService.toTagsString(this.form.value.tags.map(e => e.text));

    const formData = this.courseDto.toFormData();

    console.log(this.courseDto);


    this.courseService.add(formData)
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
    this.fileUploadMessage = this.fileToUpload.size > 0 ? 'Gotowy do wysłania' : 'Wybierz plik';
  }

  imitateFileInput() {
    document.getElementById('image').click();
  }
}
