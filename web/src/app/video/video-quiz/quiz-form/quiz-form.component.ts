import { Component, Input, OnInit } from "@angular/core";
import {
  AbstractControl,
  FormArray,
  FormControl,
  FormGroup,
  ValidationErrors,
  Validators
} from "@angular/forms";
import { VideoService } from "src/app/services/video.service";

import {
  CreateQuizPayload,
  Quiz,
  UpdateQuizPayload
} from "./../../../models/quiz.model";
import { QuizService } from "./../../../services/quiz.service";

@Component({
  selector: "app-quiz-form",
  templateUrl: "./quiz-form.component.html",
  styleUrls: ["./quiz-form.component.css"]
})
export class QuizFormComponent implements OnInit {
  quizForm: FormGroup;
  @Input() quiz?: Quiz;

  constructor(
    private videoService: VideoService,
    private quizService: QuizService
  ) {}

  ngOnInit() {
    const initialQuestions = this.parseInitialQuiz();
    this.quizForm = new FormGroup(
      {
        questions: new FormArray(initialQuestions),
        quizId: new FormControl()
      },
      [this.hasQuestions]
    );
  }

  quizQuestion(question = "", answers = []) {
    return new FormGroup({
      question: new FormControl(question, [Validators.required]),
      answers: new FormArray(answers, [
        this.questionHasAtLeastTwoAnswers,
        this.questionHasAtLeastOneCorrectAnswer
      ])
    });
  }

  quizAnswer({ answer = "", isCorrect = false } = {}) {
    return new FormGroup({
      answer: new FormControl(answer),
      isCorrect: new FormControl(isCorrect)
    });
  }

  get quizQuestions() {
    return this.quizForm.get("questions") as FormArray;
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
    const isValid =
      control.value.filter(ans => Boolean(ans.answer.trim())).length >= 2;
    if (!isValid) {
      return { minLength: true };
    }

    return null;
  }

  questionHasAtLeastOneCorrectAnswer(
    control: AbstractControl
  ): ValidationErrors {
    if (control.value.length < 2) {
      return null;
    }

    const isValid =
      control.value.filter(answer => Boolean(answer.isCorrect)).length >= 1;

    if (!isValid) {
      return { noCorrectAnswer: true };
    }
    return null;
  }

  hasQuestions(control: AbstractControl): ValidationErrors {
    const isValid = control.value.questions.length >= 1;
    if (!isValid) {
      return { atLeastOneQuestion: true };
    }
    return null;
  }

  shouldUpdateQuizOnSubmit() {
    return this.quiz && this.quiz.id;
  }

  handleSubmit(e) {
    e.preventDefault();

    const shouldUpdateQuiz = this.shouldUpdateQuizOnSubmit();
    const payload =
      this.quiz && this.quiz.id
        ? { ...this.quizForm.value, quizId: this.quiz.id }
        : this.quizForm.value;

    if (shouldUpdateQuiz) {
      this.quizService.updateQuiz(payload as UpdateQuizPayload);
    }

    this.quizService.createQuiz(payload as CreateQuizPayload).subscribe();
  }

  parseInitialQuiz() {
    if (!this.quiz || !this.quiz.questions) {
      return [];
    }
    return this.quiz.questions.map(q =>
      this.quizQuestion(q.question, q.answers.map(this.quizAnswer))
    );
  }
}
