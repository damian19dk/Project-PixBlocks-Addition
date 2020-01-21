import {Component, Input, OnInit} from '@angular/core';
import {AbstractControl, FormArray, FormControl, FormGroup, ValidationErrors, Validators} from '@angular/forms';
import {VideoService} from 'src/app/services/video.service';

import {CreateQuizPayload, Quiz, UpdateQuizPayload} from '../../../models/quiz.model';
import {QuizService} from '../../../services/quiz.service';
import {VideoDocument} from '../../../models/videoDocument.model';

@Component({
  selector: 'app-quiz-form',
  templateUrl: './quiz-form.component.html',
  styleUrls: ['./quiz-form.component.css']
})
export class QuizFormComponent implements OnInit {
  form: FormGroup;
  @Input() quiz?: Quiz;
  sent: boolean;
  error: string;
  loading = false;

  video: VideoDocument = new VideoDocument();

  constructor(
    private videoService: VideoService,
    private quizService: QuizService) {
  }

  get quizQuestions() {
    return this.form.get('questions') as FormArray;
  }

  ngOnInit() {
    const initialQuestions = this.parseInitialQuiz();
    const mediaField = this.shouldUpdateQuizOnSubmit()
      ? {}
      : {
        mediaId: new FormControl('', [Validators.required])
      };

    this.form = new FormGroup(
      {
        questions: new FormArray(initialQuestions),
        ...mediaField
      },
      [this.hasQuestions]
    );
  }

  quizQuestion(question = '', answers = []) {
    return new FormGroup({
      question: new FormControl(question, [Validators.required]),
      answers: new FormArray(answers, [
        this.questionHasAtLeastTwoAnswers,
        this.questionHasAtLeastOneCorrectAnswer
      ])
    });
  }

  quizAnswer({answer = '', isCorrect = false} = {}) {
    return new FormGroup({
      answer: new FormControl(answer),
      isCorrect: new FormControl(isCorrect)
    });
  }

  get quizQuestionsControls() {
    return this.quizQuestions.controls;
  }

  onDeleteQuestion(index: number) {
    this.quizQuestions.removeAt(index);
  }

  newQuizQuestion() {
    this.quizQuestions.push(this.quizQuestion());
  }

  questionHasAtLeastTwoAnswers(control: AbstractControl): ValidationErrors {
    // At least 2 answers which are filled with text
    const isValid = control.value.filter(ans => Boolean(ans.answer.trim())).length >= 2;
    if (!isValid) {
      return {minLength: true};
    }

    return null;
  }

  questionHasAtLeastOneCorrectAnswer(control: AbstractControl): ValidationErrors {
    if (control.value.length < 2) {
      return null;
    }

    const isValid = control.value.filter(answer => Boolean(answer.isCorrect)).length >= 1;

    if (!isValid) {
      return {noCorrectAnswer: true};
    }
    return null;
  }

  hasQuestions(control: AbstractControl): ValidationErrors {
    const isValid = control.value.questions.length >= 1;
    if (!isValid) {
      return {atLeastOneQuestion: true};
    }
    return null;
  }

  shouldUpdateQuizOnSubmit() {
    return this.quiz && this.quiz.id;
  }

  handleSubmit(e) {
    e.preventDefault();
    this.loading = true;

    const shouldUpdateQuiz = this.shouldUpdateQuizOnSubmit();
    const payload = shouldUpdateQuiz
      ? {...this.form.value, quizId: this.quiz.id}
      : {...this.form.value, mediaId: this.video.id};

    if (shouldUpdateQuiz) {
      this.quizService.update(payload as UpdateQuizPayload).subscribe(
        data => {
          this.loading = false;
          this.sent = true;
          this.error = null;
        },
        error => {
          this.loading = false;
          this.sent = true;
          this.error = error;
        }
      );
    }

    this.quizService.add(payload as CreateQuizPayload).subscribe(
      data => {
        this.loading = false;
        this.sent = true;
        this.error = null;
      },
      error => {
        this.loading = false;
        this.sent = true;
        this.error = error;
      }
    );
  }

  parseInitialQuiz() {
    if (!this.quiz || !this.quiz.questions) {
      return [];
    }
    return this.quiz.questions.map(q =>
      this.quizQuestion(q.question, q.answers.map(this.quizAnswer))
    );
  }

  selectVideo($event: any) {
    this.video = $event;
    this.form.controls.mediaId.setValue(this.video.id);
  }
}
