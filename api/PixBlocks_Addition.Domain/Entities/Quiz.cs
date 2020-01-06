using PixBlocks_Addition.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Entities
{
    public class Quiz
    {
        public Guid Id { get; protected set; }
        public Guid MediaId { get; protected set; }
        public string Question { get; protected set; }
        public ICollection<QuizAnswer> Answers { get; protected set; } = new HashSet<QuizAnswer>();

        public Quiz(string question, Guid mediaId)
        {
            Id = Guid.NewGuid();
            MediaId = mediaId;
            SetQuestion(question);
        }

        protected Quiz()
        { }

        public void SetQuestion(string question)
        {
            if(String.IsNullOrWhiteSpace(question))
            {
                throw new MyException("Incorrect question.");
            }

            if(question.Length < 4)
            {
                throw new MyException("Length must be more than 3");
            }

            Question = question;
        }

    }
}
