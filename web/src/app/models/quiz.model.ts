export class Quiz {
  id: string;
  mediaId: string;
  questions: Array<QuizQuestion>;
}

export class QuizQuestion {
  quizId: string;
  question: string;
  answers: Array<QuizAnswer>;
}

export class QuizAnswer {
  answer: string;
  isCorrect: boolean;
  isSelected: boolean;
}

export class CreateQuizPayload {
  mediaId: string;
  question: {
    question: string;
    answers: Array<QuizAnswer>;
  }[];
}

export class UpdateQuizPayload {
  quizId: string;
  question: {
    question: string;
    answers: Array<QuizAnswer>;
  }[];
}

export class Question {
  question: string;
  answers: Array<QuizAnswer>;
}
