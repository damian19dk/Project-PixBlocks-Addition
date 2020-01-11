import { Component, OnInit, Output, Input, EventEmitter } from "@angular/core";

@Component({
  selector: "app-quiz-answer",
  templateUrl: "./quiz-answer.component.html",
  styleUrls: ["./quiz-answer.component.css"]
})
export class QuizAnswerComponent implements OnInit {
  @Input() index: number;
  @Output() onRemoveAnswer = new EventEmitter<number>();

  constructor() {}

  ngOnInit() {}

  handleRemoveAnswer() {
    console.log("emnitting");
    this.onRemoveAnswer.emit(this.index);
  }
}
