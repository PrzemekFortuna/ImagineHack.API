using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuizITAPI.DB.Model
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }
       
        [Required]
        public bool IsCorrect { get; set; }
        [Required]
        public string Content { get; set; }

        [Required]
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
