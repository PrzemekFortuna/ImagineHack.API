using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuizITAPI.DB.Model;

namespace QuizITAPI.DTO
{
    public class QuestionDTO
    {
        public string Content { get; set; }
        public string UrlImage { get; set; }
        public QuestionType Type { get; set; }
        public List<AnswerDTO> Answers { get; set; }
    }
}
