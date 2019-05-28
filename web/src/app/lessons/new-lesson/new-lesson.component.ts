import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
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
  returnUrl: string;
  lessonDto: LessonDto;
  error: string;

  tagsList = [];
  tagsSettings = {};

  courses: Course[];

  constructor(private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private lessonService: LessonService,
    private tagService: TagService,
    private courseService: CourseService,
    private loadingService: LoadingService) { }

  ngOnInit() {
    this.tagsList = this.tagService.getTags();

    this.tagsSettings = this.tagService.getTagSettingsForMultiselect();

    this.lessonDto = new LessonDto();

    this.submitted = false;
    this.loading = false;

    this.newLessonForm = this.formBuilder.group({
      title: [null, Validators.required],
      description: [null, Validators.required],
      premium: [false],
      tags: [null],
      language: ['Polski'],
      parentId: [null, Validators.required],
      picture: [null]
    });

    this.getCourses();
    
  }


  get f() { return this.newLessonForm.controls; }

  getCourses() {
    this.loadingService.load();

    this.courseService.getCourses().subscribe(
      (data: Course[]) => {
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

    this.lessonService.addLesson(this.lessonDto)
      .subscribe(
        data => {
          this.loading = false;
        },
        error => {
          this.error = error;
          this.loading = false;
        });
  }

  searchCourse = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(200),
      map(term => term === '' ? []
        : this.courses.filter(v => v.title.toLowerCase().indexOf(term.toLowerCase()) > -1).slice(0, 10))
    );
  

  formatter = (x: Course) =>
    x.title;

}