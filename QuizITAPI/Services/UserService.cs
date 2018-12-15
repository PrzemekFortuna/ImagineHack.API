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
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(14),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }

        public User GetUser()
        {

        }

        public int AddUser(string email, string password)
        {
            _context.Users.Add(new User()
            {
                EMail = email,
                Password = password
            });
        }
    }
}
