using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizITAPI.DB.Model
{
    public class User
    {
        public int Id { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }

        public List<RoomUser> RoomUsers { get; set; }

    }
}
