using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizITAPI.DB.Model
{
    public class Room
    {
        public int Id { get; set; }
        public int HostId { get; set; }
        public int QuizId { get; set; }

        public User Host { get; set; }
        public List<RoomUser> RoomUsers { get; set; }
    }
}
