using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace QuizITAPI.Interfaces
{
    interface IOCRService
    {
        Task<string> MakeOCRRequest(string url);
        Task<string> MakeOCRImageRequestAsync(IFormFile image);
    }
}
