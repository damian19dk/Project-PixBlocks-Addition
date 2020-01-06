using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.ResourceModels.Quizzes
{
    public class QuizQuestionResource
    {
        public string Question { get; set; }
        public ICollection<QuizAnswerResource> Answers { get; set; }

    }
}
