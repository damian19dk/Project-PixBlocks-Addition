import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-video-quiz',
  templateUrl: './video-quiz.component.html',
  styleUrls: ['./video-quiz.component.css']
})
export class VideoQuizComponent implements OnInit {
  questions: any;
 currentIndex: number;
 notAttempted: any;
 correct: any;
 currentQuestionSet: any;
 start = false;
 gameover = false;
 buttonname = 'Play';

  constructor() {

    this.questions = [
      {
        id: 1,
        question: 'What is the full form of IP?',
        option: [
          {optionid: 1, name: 'Internet program'},
          {optionid: 2, name: 'Internet protocol'},
          {optionid: 3, name: 'Interface program'},
          {optionid: 4, name: 'Interface protocol'}
        ],
        answer: 2,
        selected: 0
      },
      {
        id: 2,
        question: 'Select the smallest memory size',
        option: [
          {optionid: 1, name: 'kilobyte'},
          {optionid: 2, name: 'megabyte'},
          {optionid: 3, name: 'gigabyte'},
          {optionid: 4, name: 'terabyte'}
        ],
        answer: 1,
        selected: 0
      },
      {
        id: 3,
        question: 'What is the full form of IP?',
        option: [
          {optionid: 1, name: 'Internet program'},
          {optionid: 2, name: 'Internet protocol'},
          {optionid: 3, name: 'Interface program'},
          {optionid: 4, name: 'Interface protocol'}
        ],
        answer: 2,
        selected: 0
      },
      {
        id: 4,
        question:'Select the smallest memory size',
        option: [
          {optionid: 1, name: 'kilobyte'},
          {optionid: 2, name: 'megabyte'},
          {optionid: 3, name: 'gigabyte'},
          {optionid: 4, name: 'terabyte'}
        ],
        answer: 1,
        selected: 0
      }
    ];
 
    this.currentIndex = 0;
    this.currentQuestionSet = this.questions[this.currentIndex];
   }

   next(){
    this.currentIndex = this.currentIndex + 1;
    this.currentQuestionSet = this.questions[this.currentIndex];
  }
  submit(){
    this.buttonname = "Replay";
    if(this.currentIndex + 1 == this.questions.length) {
       this.gameover = true;
       this.start = false;
         this.correct = 0;
    this.notAttempted=0;
    this.questions.map(x=>{
        if(x.selected!=0){
          if(x.selected == x.answer)
            this.correct=this.correct + 1;
        }
        else{
          this.notAttempted=this.notAttempted + 1;
        }
        x.selected=0;
    });
    }
    
  
  }
  startQuiz(){
    this.gameover=false;
    this.currentIndex=0;
   this.currentQuestionSet = this.questions[this.currentIndex];
      this.start=true;
  }

  ngOnInit() {
  }

}
