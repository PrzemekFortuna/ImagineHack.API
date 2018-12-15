using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizITAPI.DB.Model
{
    public class RoomUser
    {
        public int Id { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
