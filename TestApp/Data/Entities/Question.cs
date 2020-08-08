using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestApp.Data.Entities
{
    public class Question
    {
        [Key]
        public Guid Id { get; set; }

        public string Text { get; set; }

        public Guid TestId { get; set; }

        public Test Test { get; set; }

        public IList<Answer> Answers { get; set; }
    }
}
