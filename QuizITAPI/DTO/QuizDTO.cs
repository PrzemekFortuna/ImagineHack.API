using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using QuizITAPI.DB.Model;

namespace QuizITAPI.DTO
{
    public class QuizDTO
    {
        public int QuizId { get; set; }
        public Access Access { get; set; }

        public int AuthorId { get; set; }

        public List<QuestionDTO> Questions { get; set; }
    }
}
