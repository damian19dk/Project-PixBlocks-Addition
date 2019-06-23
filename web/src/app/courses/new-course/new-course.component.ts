import { Component, OnInit } from '@angular/core';
import { CourseService } from 'src/app/services/course.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CourseDto } from 'src/app/models/courseDto.model';
import { TagService } from 'src/app/services/tag.service';

@Component({
  selector: 'app-new-course',
  templateUrl: './new-course.component.html',
  styleUrls: ['./new-course.component.css']
})
export class NewCourseComponent implements OnInit {

  newCourseForm: FormGroup;
  loading: boolean;
  submitted: boolean;
  sent: boolean;
  returnUrl: string;
  courseDto: CourseDto;
  error: string;

  tagsList = [];
  tagsSettings = {};

  fileToUpload: File = null;
  fileUploadMessage: string = 'Wybierz plik';

  constructor(private formBuilder: FormBuilder,
    private courseService: CourseService,
    private tagService: TagService) { }

  ngOnInit() {
    this.tagsList = this.tagService.getTags();
    this.tagsSettings = this.tagService.getTagSettingsForMultiselect();

    this.courseDto = new CourseDto();

    this.sent = false;
    this.submitted = false;
    this.loading = false;
    this.error = null;

    this.newCourseForm = this.formBuilder.group({
      parentId: [null],
      mediaId: [null],
      title: [null, Validators.required],
      description: [null, Validators.required],
      premium: [false],
      tags: [null],
      language: ['Polski'],
      pictureUrl: [null],
      image: [null]
    });
  }


  get f() { return this.newCourseForm.controls; }

  createNewCourse() {
    this.submitted = true;

    if (this.newCourseForm.invalid) {
      return;
    }

    this.loading = true;

    this.courseDto = this.newCourseForm.value;
    this.courseDto.image = this.fileToUpload;
    let formData = new FormData();

    this.courseDto.parentId != null ? formData.append('parentId', this.courseDto.parentId) : null;
    this.courseDto.mediaId != null ? formData.append('mediaId', this.courseDto.mediaId) : null;
    this.courseDto.premium != null ? formData.append('premium', String(this.courseDto.premium)) : null;
    this.courseDto.title != null ? formData.append('title', this.courseDto.title) : null;
    this.courseDto.description != null ? formData.append('description', this.courseDto.description) : null;
    this.courseDto.pictureUrl != null ? formData.append('pictureUrl', this.courseDto.pictureUrl) : null;
    this.courseDto.image != null ? formData.append('image', this.fileToUpload) : null;
    this.courseDto.language != null ? formData.append('language', this.courseDto.language) : null;
    this.courseDto.tags != null ? formData.append('tags', this.courseDto.tags.join(" ")) : null;


    this.courseService.addCourse(formData)
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
