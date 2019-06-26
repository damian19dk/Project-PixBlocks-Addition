import { Component, OnInit } from '@angular/core';
import { LessonDocument } from 'src/app/models/lessonDocument.model';
import { CourseService } from 'src/app/services/course.service';
import { LessonService } from 'src/app/services/lesson.service';
import { LoadingService } from 'src/app/services/loading.service';

@Component({
  selector: 'app-edit-lesson',
  templateUrl: './edit-lesson.component.html',
  styleUrls: ['./edit-lesson.component.css']
})
export class EditLessonComponent implements OnInit {

  page: number = 1;
  count: number;

  lessons: LessonDocument[];
  error: string;

  constructor(
    private courseService: CourseService,
    private lessonService: LessonService,
    private loadingService: LoadingService) { }

  ngOnInit() {
    this.getLessons();
    this.getCount();
  }

  getLessons() {
    this.loadingService.load();

    this.lessonService.getAll(this.page).subscribe(
      (data: LessonDocument[]) => {
        this.lessons = data;
        this.loadingService.unload();
        },
      error => {
        this.error = error;
        this.loadingService.unload();
      }
    );
  }

  getCount() {
    return this.lessonService.count().subscribe(
      data => {
        this.count = parseInt(data);
      },
      error => {

      }
    );
  }

}
