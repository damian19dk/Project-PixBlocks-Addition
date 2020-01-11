import { VideoService } from "./../../services/video.service";
import {
  FormGroup,
  FormControl,
  FormArray,
  Validators,
  AbstractControl,
  ValidationErrors
} from "@angular/forms";
import { Component, OnInit } from "@angular/core";
import { Observable } from "rxjs";
import { debounceTime, filter, switchMap } from "rxjs/operators";

@Component({
  selector: "app-video-quiz",
  templateUrl: "./video-quiz.component.html",
  styleUrls: ["./video-quiz.component.css"]
})
export class VideoQuizComponent implements OnInit {
  initialQuiz = {
    id: "1234",
    mediaId: "string",
    questions: [
      {
        quizId: "string",
        question: "ala ma kota i",
        answers: [
          {
            answer: "dwa psy",
            isCorrect: true
          },
          {
            answer: "dwa koty",
            isCorrect: false
          },
          {
            answer: "dwa naty",
            isCorrect: false
          }
        ]
      },
      {
        quizId: "string",
        question: "ala ma kota i",
        answers: [
          {
            answer: "dwa psy",
            isCorrect: true
          },
          {
            answer: "dwa koty",
            isCorrect: false
          },
          {
            answer: "dwa naty",
            isCorrect: false
          }
        ]
      }
    ]
  };
  ngOnInit() {}
}
