using System;

namespace TestApp.Data.Entities
{
    public class UserTest
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public double Result { get; set; }

        public string UserId { get; set; }

        public Guid TestId { get; set; }

        public Test Test { get; set; }

        public User User { get; set; }
    }
}
