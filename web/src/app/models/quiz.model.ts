export type Quiz = {
  id: string;
  mediaId: string;
  questions: QuizQuestion[];
};

export type QuizQuestion = {
  quizId: string;
  question: string;
  answers: QuizAnswer[];
};

export type QuizAnswer = {
  answer: string;
  isCorrect: boolean;
};

export type CreateQuizPayload = {
  mediaId: string;
  question: {
    question: string;
    answers: QuizAnswer[];
  }[];
};

export type UpdateQuizPayload = {
  quizId: string;
  question: {
    question: string;
    answers: QuizAnswer[];
  }[];
};
