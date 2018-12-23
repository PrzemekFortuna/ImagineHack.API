using Microsoft.EntityFrameworkCore;
using QuizITAPI.DB;
using QuizITAPI.DB.Model;
using QuizITAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using QuizITAPI.Helpers;

namespace QuizITAPI.Services
{
    public class RoomsService
    {
        public QuizItContext _context;

        public RoomsService(QuizItContext context)
        {
            _context = context;
        }

        public Dictionary<string, object> GetAllRooms(int page, int pageSize)
        {
            List<RoomDTO> rooms = GetRooms(page, pageSize);

            var roomsAmount = _context.Rooms.Count();

            var result = new Dictionary<string, object>
            {
                { "rooms", rooms },
                { "total", roomsAmount }
            };

            return result;
        }

        private List<RoomDTO> GetRooms(int page, int pageSize)
        {
            return _context.Rooms
                .Select(t => new
                {
                    Room = t,
                    RoomUsers = t.RoomUsers.Select(c => new
                    {
                        c.IsHost,
                        t.RoomId,
                        c.UserId
                    })
                })
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList()
                .Select(d => new RoomDTO
                {
                    MaxUsersCount = d.Room.MaxUsersCount,
                    Name = d.Room.Name,
                    QuizId = d.Room.QuizId,
                    RoomId = d.Room.RoomId,
                    UserId = d.RoomUsers.First(c => c.IsHost == true).UserId,
                    RoomUsers = d.RoomUsers.Select(f => new RoomUser
                    {
                        IsHost = f.IsHost,
                        RoomId = f.RoomId,
                        UserId = f.UserId
                    }).ToList()
                }).ToList();
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

        public bool AddUserToRoom(int roomId, int userId)
        {

            var room = _context.Rooms.Include(r => r.RoomUsers).FirstOrDefault(r => r.RoomId == roomId);

            int currentUsers = _context.RoomUsers.Count(c => c.RoomId == roomId);
            if (currentUsers >= _context.Rooms.First(c => c.RoomId == roomId).MaxUsersCount)
                return false;

            if (_context.Rooms.Any(r => r.RoomUsers.Any(ru => ru.UserId == userId && ru.RoomId == roomId)))
                return false;

            if (room != null)
            {
                room.RoomUsers.Add(new RoomUser
                {
                    RoomId = roomId,
                    UserId = userId
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool RemoveUserFromRoom(int roomId, int userId)
        {

            var roomUser = _context.RoomUsers.FirstOrDefault(ru => ru.UserId == userId && ru.RoomId == roomId);
            if (roomUser == null)
                return false;

            _context.RoomUsers.Remove(roomUser);
            return true;
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
