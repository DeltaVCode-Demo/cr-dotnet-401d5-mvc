using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoMvc.Controllers
{
    public class AccountController : Controller
    {
        // GET Account
        public IActionResult Index()
        {
            return View();
        }

        // GET Account/Register
        public IActionResult Register()
        {
            return View();
        }
    }
}
