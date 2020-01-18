import {Component, Input, OnInit} from '@angular/core';
import {Quiz} from '../../../models/quiz.model';

@Component({
  selector: 'app-video-quiz-user',
  templateUrl: './video-quiz-user.component.html',
  styleUrls: ['./video-quiz-user.component.css']
})
export class VideoQuizUserComponent implements OnInit {
  @Input() quiz: Quiz;
  correctAnswers = 0;
  loading = false;
  submitted = false;

  constructor() {
  }

  ngOnInit() {
    this.prepareQuiz();
  }

  checkAnswers() {
    this.loading = true;
    this.correctAnswers = 0;
    let correct = true;

    for (const question of this.quiz.questions) {
      for (const answer of question.answers) {
        if (answer.isCorrect !== answer.isSelected) {
          correct = false;
          break;
        }
      }
      if (correct === true) {
        this.correctAnswers++;
      } else {
        correct = true;
      }
    }

    this.submitted = true;
    this.loading = false;
  }

  async prepareQuiz() {
    for (const question of this.quiz.questions) {
      for (const answer of question.answers) {
        answer.isSelected = false;
      }
    }
  }
}
