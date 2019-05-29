import { Component, OnInit } from '@angular/core';
import { CourseService } from 'src/app/services/course.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
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
  returnUrl: string;
  courseDto: CourseDto;
  error: string;

  tagsList = [];
  tagsSettings = {};

  constructor(private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private courseService: CourseService,
    private tagService: TagService) { }

  ngOnInit() {
    this.tagsList = this.tagService.getTags();

    this.tagsSettings = this.tagService.getTagSettingsForMultiselect();

    this.courseDto = new CourseDto();

    this.submitted = false;
    this.loading = false;

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
    let formData = new FormData();

    formData.append('premium', String(this.courseDto.premium));
    formData.append('title', this.courseDto.title);
    formData.append('description', this.courseDto.description);
    formData.append('pictureUrl', this.courseDto.pictureUrl);
    formData.append('image', this.courseDto.image);
    formData.append('language', this.courseDto.language);
    formData.append('tags', this.courseDto.tags.join(" "));
   // formData.append('parentId', this.courseDto.parentId);
   // formData.append('mediaId', this.courseDto.mediaId);

    this.courseService.addCourse(formData)
      .subscribe(
        data => {
          this.loading = false;
        },
        error => {
          this.error = error.error.message;
          this.loading = false;
        });
  }



}
