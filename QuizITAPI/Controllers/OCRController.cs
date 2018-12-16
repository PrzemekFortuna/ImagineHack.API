using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizITAPI.Services;

namespace QuizITAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OCRController : ControllerBase
    {
        private readonly OCRService _ocrService;

        public OCRController(OCRService ocrService)
        {
            _ocrService = ocrService;
        }

        [HttpPost]
        public async Task<IActionResult> GetText([FromBody] string url)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _ocrService.MakeOCRRequest(url);

            return Ok(result);
        }
    }
}