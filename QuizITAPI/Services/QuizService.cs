using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuizITAPI.DB;
using QuizITAPI.DB.Model;
using QuizITAPI.DTO;

namespace QuizITAPI.Services
{
    public class QuizService
    {
        private QuizItContext _context;
        public QuizService(QuizItContext context)
        {
            _context = context;
        }

        public List<QuizDTO> GetQuizes(int authorId, int page, int pageSize)
        {
            var quizzes = _context.Quizes
                .Where(x => x.AuthorId == authorId)
                .Select(q => new
                {
                    Quiz = q,
                    Question = q.Questions
                        .Select(qu => new
                        {
                            Answers = qu.Answers.Select(a => new
                            {
                                a.Content,
                                a.IsCorrect
                            }),
                            qu.Content,
                            qu.Type,
                            qu.UrlImage
                        })
                })
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return quizzes.Select(t => new QuizDTO()
            {
                Access = t.Quiz.Access,
                AuthorId = t.Quiz.AuthorId,
                QuizId = t.Quiz.QuizId,
                Name = t.Quiz.Name,
                Description = t.Quiz.Description,
                Questions = t.Question.Select(c => new QuestionDTO()
                {
                    Content = c.Content,
                    Type = c.Type,
                    UrlImage = c.UrlImage,
                    Answers = c.Answers.Select(a => new AnswerDTO()
                    {
                        Content = a.Content,
                        IsCorrect = a.IsCorrect
                    }).ToList()
                }).ToList()
            }).ToList();
        }

        public List<QuizDTO> GetPublicQuizes(int page, int pageSize)
        {
            var quizzes = _context.Quizes
                .Where(x => x.Access == Access.Public)
                .Select(q => new
                {
                    Quiz = q,
                    Question = q.Questions
                        .Select(qu => new
                        {
                            Answers = qu.Answers.Select(a => new
                            {
                                a.Content,
                                a.IsCorrect
                            }),
                            qu.Content,
                            qu.Type,
                            qu.UrlImage
                        })
                })
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return quizzes.Select(t => new QuizDTO()
            {
                Access = t.Quiz.Access,
                AuthorId = t.Quiz.AuthorId,
                QuizId = t.Quiz.QuizId,
                Name = t.Quiz.Name,
                Description = t.Quiz.Description,
                Questions = t.Question.Select(c => new QuestionDTO()
                {
                    Content = c.Content,
                    Type = c.Type,
                    UrlImage = c.UrlImage,
                    Answers = c.Answers.Select(a => new AnswerDTO()
                    {
                        Content = a.Content,
                        IsCorrect = a.IsCorrect
                    }).ToList()
                }).ToList()
            }).ToList();
           
        }

        public int AddQuiz(Quiz quiz)
        {
            _context.Quizes.Add(quiz);
            _context.SaveChanges();
            return quiz.QuizId;
        }

    }
}
