using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuizITAPI.DB.Model;

namespace QuizITAPI.Interfaces
{
    public interface IAuthenticator
    {
        void Authenticate(User user);
    }
}
