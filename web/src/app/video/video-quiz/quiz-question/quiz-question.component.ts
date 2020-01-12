import {FormArray, FormControl, FormGroup} from '@angular/forms';
import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-quiz-question',
  templateUrl: './quiz-question.component.html',
  styleUrls: ['./quiz-question.component.css']
})
export class QuizQuestionComponent implements OnInit {
  @Input() quizQuestionForm: FormGroup;
  @Input() quizQuestionIndex: number;
  @Output() onDeleteQuestion = new EventEmitter<number>();

  constructor() {
  }

  addQuestion() {
    this.answers.markAsDirty();
    this.answers.push(this.createAnswerForm());
  }

  get question() {
    return this.quizQuestionForm.get('question');
  }

  get formValid() {
    return this.quizQuestionForm.valid;
  }

  get answers() {
    return this.quizQuestionForm.get('answers') as FormArray;
  }

  ngOnInit() {
  }

  get answersControls() {
    return this.answers.controls;
  }

  answerFormFor(index: number) {
    return this.quizQuestionForm.value;
  }

  handleDeleteQuestion() {
    this.onDeleteQuestion.emit(this.quizQuestionIndex);
  }

  createAnswerForm() {
    return new FormGroup({
      answer: new FormControl(''),
      isCorrect: new FormControl(false)
    });
  }

  removeAnswer(index: number) {
    this.answers.removeAt(index);
  }
}
