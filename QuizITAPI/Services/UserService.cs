using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuizITAPI.DB;
using QuizITAPI.DB.Model;
using QuizITAPI.Helpers;
using QuizITAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QuizITAPI.Services
{
    public class UserService
    {
        private QuizItContext _context;

        private IAuthenticator _authenticator;

        public UserService(QuizItContext context, IAuthenticator authenticator)
        {
            _context = context;
            _authenticator = authenticator;
        }

        public User Authenticate(string email, string password)
        {
            User user = _context.Users
                .SingleOrDefault(x => x.Email == email && x.Password == password);

            if (user == null)
                return null;

            _authenticator.Authenticate(user);

            user.Password = null;

            return user;
        }

        public User GetUser(int id)
        {
            return _context.Users.FirstOrDefault(x => x.UserId == id);
        }

        public User GetUser(string email)
        {
            return _context.Users.First(x => x.Email == email);
        }

        public int AddUser(string email, string password)
        {

            User user = new User()
            {
                Email = email,
                Password = password
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            return user.UserId;
        }

        public bool UserExists(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }
    }
}
