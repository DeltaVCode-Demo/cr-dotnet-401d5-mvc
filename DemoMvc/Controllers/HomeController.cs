using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DemoMvc.Models;

namespace DemoMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Home!!");
            return View();
        }

        [HttpGet("PrivacyPolicy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet("Test/500")]
        public IActionResult Boom(string id)
        {
            throw new ApplicationException($"Boom! {id}");
        }

        [HttpGet("Test/404")]
        public IActionResult TestNotFound()
        {
            return NotFound();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
