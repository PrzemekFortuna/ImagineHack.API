using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuizITAPI.DB;
using QuizITAPI.DB.Model;

namespace QuizITAPI.Services
{
    public class QuizService
    {
        private QuizItContext _context;
        public QuizService(QuizItContext context)
        {
            _context = context;
        }

        public List<Quiz> GetQuizes(int authorId)
        {
            return _context.Quizes.Where(x => x.AuthorId == authorId).ToList();
        }

        public List<Quiz> GetPublicQuizes()
        {
            return _context.Quizes.Where(x => x.Access == Access.Public).ToList();
        }

        public int AddQuiz(Quiz quiz)
        {
            _context.Quizes.Add(quiz);
            _context.SaveChanges();
            return quiz.QuizId;
        }

    }
}
