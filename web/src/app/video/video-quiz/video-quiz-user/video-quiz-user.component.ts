import {Component, Input, OnInit} from '@angular/core';
import {Quiz} from '../../../models/quiz.model';

@Component({
  selector: 'app-video-quiz-user',
  templateUrl: './video-quiz-user.component.html',
  styleUrls: ['./video-quiz-user.component.css']
})
export class VideoQuizUserComponent implements OnInit {

  @Input() quiz: Quiz;
  testQuiz: Quiz;
  loading = false;

  constructor() {
  }

  ngOnInit() {
    this.testQuiz = new Quiz();
  }

  checkAnswers() {
    this.loading = true;
    this.loading = false;
  }
}
