using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace QuizITAPI.DB.Model
{
    public class RoomUser
    {
        public int RoomId { get; set; }
        public Room Room { get; set; }

        public bool IsHost { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
