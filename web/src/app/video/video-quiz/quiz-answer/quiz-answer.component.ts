import {FormGroup} from '@angular/forms';
import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-quiz-answer',
  templateUrl: './quiz-answer.component.html',
  styleUrls: ['./quiz-answer.component.css']
})
export class QuizAnswerComponent implements OnInit {
  @Input() quizAnswerIndex: number;
  @Input() quizAnswerForm: FormGroup;
  @Output() answerRemoved = new EventEmitter<number>();

  constructor() {
  }

  ngOnInit() {
  }

  handleRemoveAnswer() {
    this.answerRemoved.emit(this.quizAnswerIndex);
  }
}
