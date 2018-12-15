using QuizITAPI.DB;
using QuizITAPI.DB.Model;
using QuizITAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizITAPI.Services
{
    public class RoomsService
    {
        public QuizItContext _context;

        public RoomsService(QuizItContext context)
        {
            _context = context;
        }

        public int AddRoom(RoomDTO roomDTO)
        {
            Room room = new Room
            {
                Name = roomDTO.Name,
                MaxUsersCount = roomDTO.MaxUsersCount,
                Quiz = roomDTO.Quiz,
                QuizId = roomDTO.QuizId
            };

            room.RoomUsers.Add(new RoomUser
            {
                IsHost = true,
                UserId = roomDTO.UserId,
                RoomId = room.RoomId
            });

            return 0;
        }
    }
}
