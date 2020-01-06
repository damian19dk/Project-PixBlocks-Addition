using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.DTOs
{
    public class QuizDto
    {
        public Guid Id { get; set; }
        public Guid MediaId { get; set; }
        public ICollection<QuizQuestionDto> Questions { get; set; }
    }
}
