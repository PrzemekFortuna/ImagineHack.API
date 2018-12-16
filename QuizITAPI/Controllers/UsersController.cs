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
    public class UsersController : ControllerBase
    {
        private UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] User userParam)
        {
            var user = _userService.Authenticate(userParam.EMail, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user.Token);
        }


        // GET: api/Users/5
        [HttpGet("{id}")]
        public  IActionResult GetUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _userService.GetUser(id);

            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            return Ok(user);
        }

        // POST: api/Users
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult PostUser([FromBody] UserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(!_userService.UserExists(user.EMail))
            {
                int id = _userService.AddUser(user.EMail, user.Password);
                return CreatedAtAction("PostUser", new { Id = id });
            }

            return BadRequest(new { message = "User with given email already exists!" });
            
        }
     
    }
}