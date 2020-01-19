using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.DTOs
{
    public class QuizQuestionDto
    {
        public Guid QuizId { get; set; }
        public string Question { get; set; }
        public ICollection<QuizAnswerDto> Answers { get; set; }
    }
}
