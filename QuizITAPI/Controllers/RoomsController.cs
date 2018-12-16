using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizITAPI.DB;
using QuizITAPI.DB.Model;
using QuizITAPI.DTO;
using QuizITAPI.Services;

namespace QuizITAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly RoomsService _roomsService;

        public RoomsController(RoomsService roomsService)
        {
            _roomsService = roomsService;
        }


        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public IActionResult GetRoom([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var room = _roomsService.GetRoom(id);

            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        [HttpGet]
        public IEnumerable<RoomDTO> GetAllRooms()
        {
            return _roomsService.GetAllRooms();
        }
        
        [HttpPost("addroom")]
        public IActionResult PostRoom([FromBody] RoomDTO roomDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int roomId = _roomsService.AddRoom(roomDTO);

            return CreatedAtAction("PostRoom", new { id = roomId });
        }

        [HttpPost("adduser/{id}")]
        public IActionResult AddUserToRoom([FromRoute]int id, [FromBody] int userId)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!_roomsService.AddUserToRoom(id, userId))
            {
                return BadRequest(new { message = "User is already in room" });
            }

            return Ok();
        }
        [HttpPost("deleteuser/{id}")]
        public IActionResult RemoveUserFromRoom([FromRoute]int id, [FromBody] int userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_roomsService.RemoveUserFromRoom(id, userId))
            {
                return BadRequest(new { message = "User is not in room" });
            }

            return Ok();
        }
    }
}