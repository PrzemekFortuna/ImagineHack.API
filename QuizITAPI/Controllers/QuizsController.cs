using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizITAPI.DB.Model;
using QuizITAPI.DTO;
using QuizITAPI.Services;

namespace QuizITAPI.Controllers
{
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
        public ActionResult<List<QuizDTO>> GetQuizes([Required, FromQuery] int page, [Required, FromQuery] int pageSize)
        {
            var quizzes = _service.GetPublicQuizes(page, pageSize);
            return Ok(quizzes);
        }

        // GET: api/Quizs/5
        [HttpGet("{id}")]
        public IEnumerable<QuizDTO> GetQuiz([FromRoute] int id, [Required, FromQuery] int page, [Required, FromQuery] int pageSize)
        {

            return  _service.GetQuizes(id, page, pageSize);
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