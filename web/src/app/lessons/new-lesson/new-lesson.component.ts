import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { LessonDto } from 'src/app/models/lessonDto.model';
import { LessonService } from 'src/app/services/lesson.service';
import { TagService } from 'src/app/services/tag.service';

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

  constructor(private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private lessonService: LessonService,
    private tagService: TagService) { }

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
  }


  get f() { return this.newLessonForm.controls; }

  createNewLesson() {
    this.submitted = true;

    if (this.newLessonForm.invalid) {
      return;
    }

    this.loading = true;

    this.lessonDto = this.newLessonForm.value;
    console.log(this.lessonDto);

    this.loading = false;

    // this.lessonService.addLesson(this.lessonDto)
    //   .subscribe(
    //     data => {
    //       this.loading = false;
    //     },
    //     error => {
    //       this.error = error;
    //       this.loading = false;
    //     });
  }



}
