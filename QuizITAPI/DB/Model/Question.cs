﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizITAPI.DB.Model
{
    public class Question
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string UrlImage { get; set; }
        public QuestionType Type { get; set; }

        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
    }
}
