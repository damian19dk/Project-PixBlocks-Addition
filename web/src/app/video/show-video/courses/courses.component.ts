
import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CourseDocument } from 'src/app/models/courseDocument.model';
import { CourseDto } from 'src/app/models/courseDto.model';
import { CourseService } from 'src/app/services/course.service';
import { LoadingService } from '../../../services/loading.service';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-courses',
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.css']
})
export class CoursesComponent implements OnInit {

  @Input() course: CourseDocument;
  courses: CourseDocument[];
  page: number = 1;
  error: string;

 // mediaId: string;
  constructor(
    private courseService: CourseService,
    private loadingService: LoadingService,
    private route: ActivatedRoute
    ) { }


  ngOnInit() {
    this.getCourses();
  }

  getCourses() {
    this.loadingService.load();

    this.courseService.getAll(this.page).subscribe(
      (data: CourseDocument[]) => {
        this.courses = data;
        this.loadingService.unload();
        },
      error => {
        this.error = error;
        this.loadingService.unload();
      }
    );
  }/*
  getHostedCourse() {
    this.loadingService.load();

    const id = this.route.snapshot.paramMap.get('id');
    this.courseService.getCourse(id).subscribe(
      (data: CourseDocument) => {
        this.course = data;
        this.loadingService.unload();
      },
      error => {
        this.error = error;
        this.loadingService.unload();
      });
  }
  getCourse() {
    this.loadingService.load();

    const mediaId = this.route.snapshot.paramMap.get('id');
    this.courseService.getCourse(mediaId).subscribe(
      (data: CourseDocument) => {
     //   this.courseDocument = data;
        this.loadingService.unload();
      },
      error => {
        this.error = error;
        this.loadingService.unload();
      });
  }*/

}
