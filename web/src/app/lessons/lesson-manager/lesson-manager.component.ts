import { Component, OnInit } from '@angular/core';
import { LessonService } from 'src/app/services/lesson.service';
import { LoadingService } from 'src/app/services/loading.service';
import { LessonDocument } from 'src/app/models/lessonDocument.model';

@Component({
  selector: 'app-lesson-manager',
  templateUrl: './lesson-manager.component.html',
  styleUrls: ['./lesson-manager.component.css']
})
export class LessonManagerComponent implements OnInit {

  lesson: LessonDocument;
  error: string;
  isNewLessonSelected: boolean;

  constructor(
    private lessonService: LessonService,
    private loadingService: LoadingService) { }

  ngOnInit() {
    this.isNewLessonSelected = true;
  }

  getCourse() {
    this.loadingService.load();
    this.lessonService.getOne("Sto twarzy grzybiarzy")
      .subscribe(
        (data: LessonDocument) => {
          this.lesson = data;
          this.loadingService.unload();
        },
        error => {
          this.error = error;
          this.loadingService.unload();
        }
      );
  }

  changeToEditLesson() {
    this.isNewLessonSelected = false;
  }

  changeToNewLesson() {
    this.isNewLessonSelected = true;
  }

}
