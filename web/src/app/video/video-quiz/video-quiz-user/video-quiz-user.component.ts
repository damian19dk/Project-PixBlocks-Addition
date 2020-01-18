import {Component, Input, OnInit} from '@angular/core';
import {Quiz} from '../../../models/quiz.model';
import {QuizService} from '../../../services/quiz.service';

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

  constructor(private quizService: QuizService) {
  }

  ngOnInit() {
    this.getQuiz();
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

  async getQuiz() {
    this.quizService.getOne('A6105B14-4C97-4634-A1D3-04960F041B2C').subscribe(
      (data: Quiz) => {
        this.quiz = data;

        for (const question of this.quiz.questions) {
          for (const answer of question.answers) {
            answer.isSelected = false;
          }
        }
      }
    );
  }
}
