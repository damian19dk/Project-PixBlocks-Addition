using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Entities
{
    public class QuizAnswer
    {
        public Guid Id { get; protected set; }
        public Guid QuizQuestionId { get; protected set; }
        public string Answer { get; private set; }
        public bool IsCorrect { get; private set; }

        public QuizAnswer(string answer, bool isCorrectAnswer)
        {
            Id = Guid.NewGuid();
            Answer = answer;
            IsCorrect = isCorrectAnswer;
        }

        protected QuizAnswer()
        { }
    }
}
