using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using QAHomework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QAHomework.Web.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly string _connectionString;

        public QuestionsController(IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            _environment = environment;
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult AskAQuestion()
        {
            return View();
        }

        [Authorize]
        public IActionResult AddQuestion(Question question, IEnumerable<string> tags)
        {
            QuestionsRepository questionRepo = new QuestionsRepository(_connectionString);
            UserRepository userRepo = new UserRepository(_connectionString);
            question.User = userRepo.GetByEmail(User.Identity.Name);
            question.DatePosted = DateTime.Now;
            questionRepo.AddQuestionWithTags(question, tags);
            return Redirect("/");
        }

    }
}
