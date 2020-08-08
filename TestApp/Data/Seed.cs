using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Data.Entities;

namespace TestApp.Data
{
    public static class Seed
    {
        public static async Task<IApplicationBuilder> UseSeedAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context =
                    (ApplicationDbContext)scope.ServiceProvider.GetService(typeof(ApplicationDbContext));

                var userManager = (UserManager<User>)scope.ServiceProvider.GetService(typeof(UserManager<User>));

                var user = new User
                {
                    Email = "test@test.com",
                    NormalizedEmail = "test@test.com",
                    UserName = "Owner",
                    NormalizedUserName = "OWNER",
                    PhoneNumber = "+111111111111",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                var Test = new Test
                {
                    Description = "test to verify math skills",
                    Title = "Math",
                    Questions = new List<Question>
                    {
                        new Question()
                        {
                            Text = "2+2", Answers = new List<Answer>()
                            {
                                new Answer() {IsRight = true, Text = "4"},
                                new Answer() {IsRight = false, Text = "5"},
                                new Answer() {IsRight = false, Text = "6"},
                                new Answer() {IsRight = false, Text = "7"},
                            }
                        },
                        new Question()
                        {
                            Text = "10+2", Answers = new List<Answer>()
                            {
                                new Answer() {IsRight = false, Text = "1654"},
                                new Answer() {IsRight = true, Text = "12"},
                                new Answer() {IsRight = false, Text = "test"},
                                new Answer() {IsRight = false, Text = "qweerty"},
                            }
                        }
                    },
                    UserTest = new List<UserTest>()
                    {
                        new UserTest(){User = user, Date = DateTime.Now},
                    },
                };

                var Test1 = new Test
                {
                    Description = "Test to verify your physics knowledge",
                    Title = "Physics",
                    Questions = new List<Question>
                    {
                        new Question()
                        {
                            Text = "Координата тела меняется с течением времени согласно формуле x = 5-3t, где все величины выражены в СИ. Чему равна координата этого тела через 5 с после начала движения?",
                            Answers = new List<Answer>()
                            {
                                new Answer() {IsRight = true, Text = "-15 м"},
                                new Answer() {IsRight = false, Text = "-10 м"},
                                new Answer() {IsRight = false, Text = "10 м"},
                                new Answer() {IsRight = false, Text = "15 м"},
                            }
                        },
                        new Question()
                        {
                            Text = "10+2", Answers = new List<Answer>()
                            {
                                new Answer() {IsRight = false, Text = "1654"},
                                new Answer() {IsRight = true, Text = "12"},
                                new Answer() {IsRight = false, Text = "test"},
                                new Answer() {IsRight = false, Text = "qweerty"},
                            }
                        }
                    },
                    UserTest = new List<UserTest>()
                    {
                        new UserTest(){User = user, Date = DateTime.Now},
                    },
                };

                if (!context.Users.Any(u => u.UserName == user.UserName))
                {
                    await userManager.CreateAsync(user, "123Qwe!");
                }

                context.Tests.Add(Test1);
                context.Tests.Add(Test);
                context.SaveChanges();
                return app;
            }
        }
    }
}
