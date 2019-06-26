import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { LessonDto } from 'src/app/models/lessonDto.model';
import { LessonService } from 'src/app/services/lesson.service';
import { TagService } from 'src/app/services/tag.service';
import { CourseService } from 'src/app/services/course.service';
import { Observable } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { CourseDocument } from 'src/app/models/courseDocument.model';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-new-lesson',
  templateUrl: './new-lesson.component.html',
  styleUrls: ['./new-lesson.component.css']
})
export class NewLessonComponent implements OnInit {

  newLessonForm: FormGroup;
  loading: boolean;
  submitted: boolean;
  sent: boolean;
  returnUrl: string;
  lessonDto: LessonDto;
  error: string;

  tagsList = [];
  tagsSettings = {};

  courses: any[];
  fileToUpload: File = null;
  fileUploadMessage: string;

  constructor(private formBuilder: FormBuilder,
    private lessonService: LessonService,
    private tagService: TagService,
    private courseService: CourseService) { }

  ngOnInit() {
    this.tagsList = this.tagService.getTags();

    this.tagsSettings = this.tagService.getTagSettingsForMultiselect();

    this.lessonDto = new LessonDto();

    this.submitted = false;
    this.sent = false;
    this.loading = false;

    this.newLessonForm = this.formBuilder.group({
      title: [null, Validators.required],
      description: [null, [Validators.required, Validators.minLength(3), Validators.maxLength(10000)]],
      premium: [false],
      tags: [null],
      language: ['Polski'],
      parentId: [null, Validators.required],
      pictureUrl: [null],
      image: [null]
    });
  }


  get f() { return this.newLessonForm.controls; }

  createNewLesson() {
    this.submitted = true;

    if (this.newLessonForm.invalid) {
      return;
    }

    this.loading = true;

    this.lessonDto = this.newLessonForm.value;
    this.lessonDto.parentId = this.newLessonForm.value.parentId.id;
    this.lessonDto.image = this.fileToUpload;

    let formData = new FormData();
    
    this.lessonDto.parentId != null ? formData.append('parentId', this.lessonDto.parentId) : null;
    this.lessonDto.id != null ? formData.append('id', this.lessonDto.id) : null;
    this.lessonDto.premium != null ? formData.append('premium', String(this.lessonDto.premium)) : null;
    this.lessonDto.title != null ? formData.append('title', this.lessonDto.title) : null;
    this.lessonDto.description != null ? formData.append('description', this.lessonDto.description) : null;
    this.lessonDto.pictureUrl != null ? formData.append('pictureUrl', this.lessonDto.pictureUrl) : null;
    this.lessonDto.image != null ? formData.append('image', this.fileToUpload) : null;
    this.lessonDto.language != null ? formData.append('language', this.lessonDto.language) : null;
    this.lessonDto.tags != null ? formData.append('tags', this.lessonDto.tags) : null;

    this.lessonService.add(formData)
      .subscribe(
        data => {
          this.sent = true;
          this.loading = false;
        },
        error => {
          this.error = error;
          this.sent = true;
          this.loading = false;
        });
  }

  searchCourse = (text$: Observable<string>) => {
    return text$.pipe(
      debounceTime(300),
      switchMap((searchText) => this.courseService.findByTitle(searchText))
    );
  }

  formatter = (x: CourseDocument) =>
    x.title;


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
