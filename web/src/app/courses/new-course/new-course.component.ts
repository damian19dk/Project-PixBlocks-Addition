import { Component, OnInit } from '@angular/core';
import { CourseService } from 'src/app/services/course.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CourseDto } from 'src/app/models/courseDto.model';

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
    private courseService: CourseService) { }

  ngOnInit() {    
    this.tagsList = [
      "Ruby", "Haskell", "Java"
    ];
    this.tagsSettings = {
      singleSelection: false,
      selectAllText: 'Zaznacz wszystkie',
      unSelectAllText: 'Odznacz wszystkie',
      itemsShowLimit: 5,
      allowSearchFilter: true,
      searchPlaceholderText: 'Szukaj...'
    };




    this.courseDto = new CourseDto();

    this.submitted = false;
    this.loading = false;

    this.newCourseForm = this.formBuilder.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      premium: [''],
      tags: [''],
      language: ['Polski'],
      parentName: [''],
      picture: ['']
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
    console.log(this.courseDto);

    this.loading = false;

    this.courseService.addCourse(this.courseDto)
      .subscribe(
        data => {
          this.loading = false;
        },
        error => {
          this.error = error;
          this.loading = false;
        });
  }

  

}
