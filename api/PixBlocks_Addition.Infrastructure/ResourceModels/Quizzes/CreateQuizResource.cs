using PixBlocks_Addition.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.ResourceModels.Quizzes
{
    public class CreateQuizResource
    {
        public Guid MediaId { get; set; }
        public ICollection<QuizQuestionResource> Questions { get; set; }
    }
}
