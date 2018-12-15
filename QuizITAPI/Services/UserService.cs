using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuizITAPI.DB;
using QuizITAPI.DB.Model;
using QuizITAPI.Helpers;
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
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, QuizItContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public User Authenticate(string email, string password)
        {
            User user = _context.Users.SingleOrDefault(x => x.EMail == email && x.Password == password);
            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(14),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            user.Password = null;

            return user;
        }

        public User GetUser(int id)
        {
            return _context.Users.FirstOrDefault(x => x.UserId == id);
        }

        public User GetUser(string email)
        {
            return _context.Users.First(x => x.EMail == email);
        }

        public int AddUser(string email, string password)
        {
            User user = new User()
            {
                EMail = email,
                Password = password
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            return user.UserId;
        }
    }
}
