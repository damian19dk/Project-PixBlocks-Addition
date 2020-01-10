export type Quiz = {
  id: string
  mediaId: string
  question: QuizQuestion[]
}

export type QuizQuestion = {
  quizId: string
  question: string
  answers: QuizAnswer[]
}

export type QuizAnswer = {
  answer: string
  isCorrect: boolean
}
