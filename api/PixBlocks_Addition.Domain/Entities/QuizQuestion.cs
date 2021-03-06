﻿using PixBlocks_Addition.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Entities
{
    public class QuizQuestion
    {
        public Guid Id { get; protected set; }
        public Guid QuizId { get; protected set; }
        public string Question { get; protected set; }
        public ICollection<QuizAnswer> Answers { get; protected set; } = new HashSet<QuizAnswer>();

        public QuizQuestion(string question)
        {
            Id = Guid.NewGuid();
            SetQuestion(question);
        }

        protected QuizQuestion()
        { }

        public void SetQuestion(string question)
        {
            if (String.IsNullOrWhiteSpace(question))
            {
                throw new MyException(MyCodesNumbers.InvalidQuestion, Exceptions.ExceptionMessages.DomainExceptionMessages.InvalidQuestion);
            }
            if (question.Length < 3)
            {
                throw new MyException(MyCodesNumbers.InvalidQuestion, Exceptions.ExceptionMessages.DomainExceptionMessages.InvalidQuestion);
            }

            Question = question;
        }
    }
}
