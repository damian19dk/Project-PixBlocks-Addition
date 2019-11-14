/*import { Component, OnInit, Input } from '@angular/core';
import { HostedCourseDocument } from '../../../models/hostedCourseDocument.model';
import { CourseService } from '../../../services/course.service';
import { ActivatedRoute } from '@angular/router';
import { LoadingService } from '../../../services/loading.service';
import { CourseDocument } from 'src/app/models/courseDocument.model';


@Component({
  selector: 'app-courses',
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.css']
})
export class CoursesComponent implements OnInit {
  @Input() course: CourseDocument = null;
  @Output() editCourseComponent: EventEmitter<any> = new EventEmitter<any>();
  image: any;
  error: string;
 // mediaId: string;
  editCourseForm: FormGroup;
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

  constructor(
    private route: ActivatedRoute,
    private courseService: CourseService,
    private loadingService: LoadingService,
    private modalService: NgbModal,
    private formBuilder: FormBuilder,
  ) { }

  ngOnInit() {
    this.getCourses();
    this.getPicture();

    this.tagsList = this.tagService.getTags();
    this.tagsSettings = this.tagService.getTagSettingsForMultiselect();

    this.courseDto = new CourseDto();

    this.sent = false;
    this.submitted = false;
    this.loading = false;
    this.error = null;

    this.editCourseForm = this.formBuilder.group({
      parentId: [null],
      id: [this.course.id],
      title: [this.course.title, Validators.required],
      description: [this.course.description, [Validators.required, Validators.minLength(3), Validators.maxLength(10000)]],
      premium: [this.course.premium],
      tags: [this.tagService.toTagsList(this.course.tags)],
      language: [this.course.language],
      pictureUrl: [this.course.picture],
      image: [null]
    });
  }

  getCourses() {
    this.loadingService.load();
    const mediaId = this.route.snapshot.paramMap.get('id');
    this.courseService.getCourse(mediaId).subscribe(
      (data: CourseDocument) => {
        this.courseDocument = data;
        this.loadingService.unload();
        },
      error => {
        this.error = error;
        this.loadingService.unload();
      }
    );
  }
  private getPicture() {
    let picture = null;
    if (this.course.picture == null) {
      this.course.picture = "https://mdrao.ca/wp-content/uploads/2018/03/DistanceEdCourse_ResitExam.png";
      return;
    }
  }

}
*/
import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CourseDocument } from 'src/app/models/courseDocument.model';
import { CourseDto } from 'src/app/models/courseDto.model';
import { CourseService } from 'src/app/services/course.service';
import { LoadingService } from '../../../services/loading.service';
import { ActivatedRoute } from '@angular/router';
// import { HostedCourseDocument } from '../../models/hostedCourseDocument.model';

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
  }
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
  }

}
