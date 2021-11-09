using DemoMvc.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoMvc.Controllers
{
    public class AdminController : Controller
    {
        private readonly IDashboardRepository dashboard;

        public AdminController(IDashboardRepository dashboard)
        {
            this.dashboard = dashboard;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
