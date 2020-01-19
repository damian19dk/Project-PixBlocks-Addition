using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Infrastructure.ResourceModels.Quizzes
{
    public class QuizAnswerResource
    {
        public string Answer { get; set; }
        public bool IsCorrect { get; set; }
    }
}
