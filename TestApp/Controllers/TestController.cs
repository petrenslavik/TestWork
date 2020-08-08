using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TestApp.Data;
using TestApp.Data.Entities;
using TestApp.Models;

namespace TestApp.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;

        public TestController(ApplicationDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetTests()
        {
            var userId = userManager.GetUserId(HttpContext.User);
            var userTests = context.UserTests.Where(t => t.UserId == userId).Include(t => t.Test).ToList();
            return Json(userTests.Select(t =>
            {
                t.Test.UserTest = null;
                return t.Test;
            }));
        }

        public IActionResult Start(Guid id)
        {
            var test = context.Tests.Include(t=>t.Questions).ThenInclude(t=>t.Answers).Single(t => t.Id == id);
            return Json(test);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
