using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace QuizITAPI.DB.Model
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string EMail { get; set; }
        [Required]
        public string Password { get; set; }

        public List<RoomUser> RoomUsers { get; set; }
        public List<Quiz> Quizes { get; set; }

    }
}
