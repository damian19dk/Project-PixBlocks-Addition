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
        public ICollection<QuizQuestion> Questions { get; protected set; } = new HashSet<QuizQuestion>();

        public Quiz(Guid mediaId)
        {
            Id = Guid.NewGuid();
            MediaId = mediaId;
        }

        protected Quiz()
        { }

    }
}
