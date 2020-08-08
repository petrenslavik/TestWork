using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestApp.Data.Entities
{
    public class Test
    {
        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public IList<Question> Questions { get; set; }

        public IList<UserTest> UserTest { get; set; }
    }
}
