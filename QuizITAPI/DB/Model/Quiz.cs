using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuizITAPI.DB.Model
{
    public class Quiz
    {
        [Key]
        public int QuizId { get; set; }
        [Required]
        public Access Access { get; set; }

        [Required]
        public int AuthorId { get; set; }
        public User Author { get; set; }

        public List<Room> Rooms{ get; set; }
    }
}
