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
        private static string QuizzesKey = "quizzes";
        private static string TotalKey = "total";

        private QuizItContext _context;
        public QuizService(QuizItContext context)
        {
            _context = context;
        }

        public Dictionary<string, object> GetQuizes(int authorId, int page, int pageSize)
        {
            List<QuizDTO> quizzes = GetUserQuizesQuery(authorId, page, pageSize);

            var total = _context.Quizes.Count();

            var result = new Dictionary<string, object>()
            {
                {QuizzesKey, quizzes },
                {TotalKey,  total}
            };

            return result;
        }

        private List<QuizDTO> GetUserQuizesQuery(int authorId, int page, int pageSize)
        {
            return _context.Quizes
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
                            .Select(t => new QuizDTO()
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

        public Dictionary<string, object> GetPublicQuizes(int page, int pageSize)
        {
            List<QuizDTO> quizzes = GetPublicQuizzesQuery(page, pageSize);

            var total = _context.Quizes.Count();

            return new Dictionary<string, object>()
            {
                {QuizzesKey, quizzes },
                {TotalKey, total }
            };
        }

        private List<QuizDTO> GetPublicQuizzesQuery(int page, int pageSize)
        {
            return _context.Quizes
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
                            .Select(t => new QuizDTO()
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
