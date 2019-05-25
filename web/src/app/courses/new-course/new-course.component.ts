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
  
  dropdownList = [];
  selectedItems = [];
  dropdownSettings = {};

  constructor(private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private courseService: CourseService) { }

  ngOnInit() {    
    this.dropdownList = [
      { item_id: 1, item_text: 'Haskell' },
      { item_id: 2, item_text: 'Python' },
      { item_id: 3, item_text: 'JavaScript' },
      { item_id: 4, item_text: 'Java' },
      { item_id: 5, item_text: 'Ruby' }
    ];
    this.dropdownSettings = {
      singleSelection: false,
      idField: 'item_id',
      textField: 'item_text',
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
      language: [''],
      parentName: [''],
      picture: ['']
    });

    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
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

    // this.courseService.addCourse(this.courseDto)
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
