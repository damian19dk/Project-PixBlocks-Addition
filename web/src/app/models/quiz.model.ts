export class Quiz {
  id: string;
  mediaId: string;
  questions: Array<QuizQuestion>;
}

export interface QuizQuestion {
  quizId: string;
  question: string;
  answers: Array<QuizAnswer>;
}

export interface QuizAnswer {
  answer: string;
  isCorrect: boolean;
}

export interface CreateQuizPayload {
  mediaId: string;
  question: {
    question: string;
    answers: Array<QuizAnswer>;
  }[];
}

export interface UpdateQuizPayload {
  quizId: string;
  question: {
    question: string;
    answers: Array<QuizAnswer>;
  }[];
}

export interface Question {
  question: string;
  answers: Array<QuizAnswer>;
}
