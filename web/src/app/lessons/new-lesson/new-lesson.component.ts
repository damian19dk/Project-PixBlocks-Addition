import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { LessonDto } from 'src/app/models/lessonDto.model';
import { LessonService } from 'src/app/services/lesson.service';
import { TagService } from 'src/app/services/tag.service';
import { CourseService } from 'src/app/services/course.service';
import { Observable } from 'rxjs';
import { debounceTime, map } from 'rxjs/operators';
import { Course } from 'src/app/models/course.model';
import { LoadingService } from 'src/app/services/loading.service';

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

  constructor(private formBuilder: FormBuilder,
    private lessonService: LessonService,
    private tagService: TagService,
    private courseService: CourseService,
    private loadingService: LoadingService) { }
    
  ngOnInit() {
    this.tagsList = this.tagService.getTags();

    this.tagsSettings = this.tagService.getTagSettingsForMultiselect();

    this.lessonDto = new LessonDto();

    this.submitted = false;
    this.sent = false;
    this.loading = false;

    this.newLessonForm = this.formBuilder.group({
      title: [null, Validators.required],
      description: [null, Validators.required],
      premium: [false],
      tags: [null],
      language: ['Polski'],
      parentId: [null, Validators.required],
      pictureUrl: [null],
      image: [null]
    });

    this.getCourses();
    
  }


  get f() { return this.newLessonForm.controls; }

  getCourses() {
    this.loadingService.load();

    this.courseService.getCourses().subscribe(
      (data: []) => {
        this.courses = data;
        this.loadingService.unload();
      },
      error => {
        this.error = error;
        this.loadingService.unload();
      }
    );
  }

  createNewLesson() {
    this.submitted = true;

    if (this.newLessonForm.invalid) {
      return;
    }

    this.loading = true;

    this.lessonDto = this.newLessonForm.value;
    this.lessonDto.parentId = this.newLessonForm.value.parentId.id;

    let formData = new FormData();

    this.lessonDto.parentId != null ? formData.append('parentId', this.lessonDto.parentId) : null;
    this.lessonDto.mediaId != null ? formData.append('mediaId', this.lessonDto.mediaId) : null;
    this.lessonDto.premium != null ? formData.append('premium', String(this.lessonDto.premium)) : null;
    this.lessonDto.title != null ? formData.append('title', this.lessonDto.title) : null;
    this.lessonDto.description != null ? formData.append('description', this.lessonDto.description) : null;
    this.lessonDto.pictureUrl != null ? formData.append('pictureUrl', this.lessonDto.pictureUrl) : null;
    this.lessonDto.image != null ? formData.append('image', this.lessonDto.image) : null;
    this.lessonDto.language != null ? formData.append('language', this.lessonDto.language) : null;
    this.lessonDto.tags != null ? formData.append('tags', this.lessonDto.tags.join(" ")) : null;

    this.lessonService.addLesson(formData)
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

  searchCourse = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(400),
      map(term => term === '' ? []
        : this.courses
        .filter(course => course != null)
        .filter(course => course.title != null)
        .filter(course => course.title.toLowerCase()
        .indexOf(term.toLowerCase()) > -1).slice(0, 10))
    );
  

  formatter = (x: Course) =>
    x.title;

}
