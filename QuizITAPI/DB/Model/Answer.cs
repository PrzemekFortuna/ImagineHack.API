using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizITAPI.DB.Model
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public bool IsCorrect { get; set; }
        public string Content { get; set; }

        public Question Question { get; set; }
    }
}
