﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizITAPI.DB.Model
{
    public class Quiz
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Access Access { get; set; }
    }
}
