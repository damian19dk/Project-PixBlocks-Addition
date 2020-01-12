export interface Quiz {
  id: string;
  mediaId: string;
  questions: QuizQuestion[];
}

export interface QuizQuestion {
  quizId: string;
  question: string;
  answers: QuizAnswer[];
}

export interface QuizAnswer {
  answer: string;
  isCorrect: boolean;
}

export interface CreateQuizPayload {
  mediaId: string;
  question: {
    question: string;
    answers: QuizAnswer[];
  }[];
}

export interface UpdateQuizPayload {
  quizId: string;
  question: {
    question: string;
    answers: QuizAnswer[];
  }[];
}
