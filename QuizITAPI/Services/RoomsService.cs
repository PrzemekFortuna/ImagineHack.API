using Microsoft.EntityFrameworkCore;
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
                QuizId = roomDTO.QuizId,
                RoomUsers = new List<RoomUser>()
            };

            room.RoomUsers.Add(new RoomUser
            {
                IsHost = true,
                UserId = roomDTO.UserId,
            });

            _context.Rooms.Add(room);
            _context.SaveChanges();

            return room.RoomId;
        }

        public RoomDTO GetRoom(int id)
        {
            var room = _context.Rooms
                .Where(r => r.RoomId == id)
                .Select(r => new RoomDTO
                {
                    MaxUsersCount = r.MaxUsersCount,
                    Name = r.Name,
                    QuizId = r.QuizId,
                    RoomId = r.RoomId,
                    RoomUsers = _context.RoomUsers
                    .Where(ru => ru.RoomId == r.RoomId)
                    .Select(ru => new RoomUser
                    {
                        IsHost = ru.IsHost,
                        RoomId = ru.RoomId,
                        UserId = ru.UserId
                    }).ToList()

                }).FirstOrDefault();

            return room;
        }

        public RoomDTO GetRoom(string name)
        {
            var room = _context.Rooms
                .Where(r => r.Name == name)
                .Select(r => new RoomDTO
                {
                    MaxUsersCount = r.MaxUsersCount,
                    Name = r.Name,
                    QuizId = r.QuizId,
                    RoomId = r.RoomId,
                    RoomUsers = _context.RoomUsers
                    .Where(ru => ru.RoomId == r.RoomId)
                    .Select(ru => new RoomUser
                    {
                        IsHost = ru.IsHost,
                        RoomId = ru.RoomId,
                        UserId = ru.UserId
                    }).ToList()
                }).FirstOrDefault();

                return room;
        }
    }
}
