using QuizITAPI.DB;
using QuizITAPI.DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizITAPI.Services
{
    public class UserService
    {
        private QuizItContext _context;

        public UserService(QuizItContext context)
        {
            _context = context;
        }

        public User GetUser()
        {

        }

        public int AddUser(string email, string password)
        {
            
        }
    }
}
