using QuizITAPI.DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizITAPI.DTO
{
    public class RoomDTO
    {
        public int MaxUsersCount { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public int QuizId { get; set; }
        public int RoomId { get; set; }
        public List<RoomUser> RoomUsers { get; set; }
    }
}
