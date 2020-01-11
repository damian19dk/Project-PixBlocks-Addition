import { QuizAnswer } from "./../../models/quiz.model";
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
      questions: new FormArray([])
    });
  }

  quizQuestion() {
    return new FormGroup({
      question: new FormControl("", [Validators.required]),
      answers: new FormArray([], [this.validateAnswers])
    });
  }

  quizAnswer() {
    return new FormGroup({
      isCorrect: new FormControl(),
      answer: new FormControl()
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

  validateAnswers(control: AbstractControl): ValidationErrors {
    if (control.value.length < 1) return { minLength: true };
    return null;
  }

  handleSubmit() {
    console.log(this.quizForm);
  }
}
