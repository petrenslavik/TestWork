using System;
using System.ComponentModel.DataAnnotations;

namespace TestApp.Data.Entities
{
    public class Answer
    {
        [Key]
        public Guid Id { get; set; }

        public string Text { get; set; }

        public bool IsRight { get; set; }

        public Guid QuestionId { get; set; }

        public Question Question { get; set; }

    }
}
