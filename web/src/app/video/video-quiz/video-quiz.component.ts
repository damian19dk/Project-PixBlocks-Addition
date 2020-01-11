import {
  FormGroup,
  FormControl,
  FormArray,
  Validators,
  AbstractControl,
  ValidationErrors
} from "@angular/forms";
import { Component, OnInit } from "@angular/core";

@Component({
  selector: "app-video-quiz",
  templateUrl: "./video-quiz.component.html",
  styleUrls: ["./video-quiz.component.css"]
})
export class VideoQuizComponent implements OnInit {
  quizForm: FormGroup;

  constructor() {}

  ngOnInit() {
    this.quizForm = new FormGroup({
      questions: new FormArray([], [Validators.required])
    });
  }

  quizQuestion() {
    return new FormGroup({
      question: new FormControl("", [Validators.required]),
      answers: new FormArray(
        [],
        [
          this.questionHasAtLeastTwoAnswers,
          this.questionHasAtLeastOneCorrectAnswer
        ]
      )
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

    return { minLength: false };
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
    return { noCorrectAnswer: false };
  }

  handleSubmit() {
    const { questions } = this.quizForm.value;
    console.log(this.quizForm.value);
  }
}
