using DemoMvc.Models.Identity;
using DemoMvc.Services;
using DemoMvc.Services.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoMvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        private readonly IFileUploadService fileUploadService;

        public AccountController(IUserService userService, IFileUploadService fileUploadService)
        {
            this.userService = userService;
            this.fileUploadService = fileUploadService;
        }

        // GET Account
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        // GET Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST Account/Register
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterData data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }

            await userService.Register(data, ModelState);

            if (!ModelState.IsValid)
            {
                return View(data);
            }

            // Post - Redirect - Get to avoid "do you want to resubmit?"
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginData data)
        {
            var user = await userService.Authenticate(data);
            if (user != null)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(nameof(LoginData.Password), "Email or Password was incorrect.");

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> UploadProfile(IFormFile profileImage)
        {
            string url = await fileUploadService.Upload(profileImage);
            return RedirectToAction(nameof(Index));
        }
    }
}
