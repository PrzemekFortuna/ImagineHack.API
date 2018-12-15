using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace QuizITAPI.DB.Model
{
    public class Room
    {
        public int RoomId { get; set; }

        public int QuizId { get; set; }
        public int MaxUsersCount { get; set; }
        public string Name { get; set; }
        public Quiz Quiz { get; set; }

        public List<RoomUser> RoomUsers { get; set; }
    }
}
