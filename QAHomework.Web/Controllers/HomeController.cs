using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QAHomework.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using QAHomework.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace QAHomework.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly string _connectionString;

        public HomeController(IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            _environment = environment;
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        public IActionResult Index()
        {
            QuestionsRepository repo = new QuestionsRepository(_connectionString);
            QuestionsViewModel vm = new QuestionsViewModel()
            {
                Questions = repo.GetAllQuestions()
            };
            return View(vm);
        }

    }
}
