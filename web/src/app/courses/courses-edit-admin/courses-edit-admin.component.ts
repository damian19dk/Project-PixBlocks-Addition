import {Component, OnInit} from '@angular/core';
import {CourseDocument} from '../../models/courseDocument.model';
import {CourseService} from '../../services/course.service';
import {LoadingService} from '../../services/loading.service';
import {CdkDragDrop, moveItemInArray} from '@angular/cdk/drag-drop';

@Component({
  selector: 'app-courses-edit-admin',
  templateUrl: './courses-edit-admin.component.html',
  styleUrls: ['./courses-edit-admin.component.css']
})
export class CoursesEditAdminComponent implements OnInit {

  page = 1;
  count: number;

  courses: Array<CourseDocument>;
  error: string;

  constructor(
    private courseService: CourseService,
    private loadingService: LoadingService) {
  }

  ngOnInit() {
    this.getCourses();
    this.getCount();
  }

  getCourses() {
    this.loadingService.load();

    this.courseService.getAll(this.page).subscribe(
      (data: Array<CourseDocument>) => {
        this.courses = data;
        this.loadingService.unload();
      },
      error => {
        this.error = error;
        this.loadingService.unload();
      }
    );
  }

  getCount() {
    return this.courseService.count().subscribe(
      data => {
        // tslint:disable-next-line:radix
        this.count = parseInt(data);
      }
    );
  }

  drop(event: CdkDragDrop<string[]>) {
    moveItemInArray(this.courses, event.previousIndex, event.currentIndex);
    this.courseService.changeOrder(this.courses.map(e => e.id)).subscribe(
      data => {
      },
      error => {
        this.error = error;
      }
    );
  }

}

