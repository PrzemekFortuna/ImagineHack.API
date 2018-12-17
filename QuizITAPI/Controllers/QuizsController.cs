using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizITAPI.DB.Model;
using QuizITAPI.DTO;
using QuizITAPI.Services;

namespace QuizITAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuizsController : ControllerBase
    {
        private readonly QuizService _service;

        public QuizsController(QuizService service)
        {
            _service = service;
        }

        // GET: api/Quizs
        [HttpGet]
        public IEnumerable<QuizDTO> GetQuizes()
        {
            return _service.GetPublicQuizes();
        }

        // GET: api/Quizs/5
        [HttpGet("{id}")]
        public IEnumerable<QuizDTO> GetQuiz([FromRoute] int id)
        {

            return  _service.GetQuizes(id);
        }

        // POST: api/Quizs
        [HttpPost]
        public IActionResult AddQuiz([FromBody] Quiz quiz)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = _service.AddQuiz(quiz);
            return CreatedAtAction("AddQuiz", new { Id = id });
        }
    }
}