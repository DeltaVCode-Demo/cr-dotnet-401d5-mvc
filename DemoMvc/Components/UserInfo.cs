using DemoMvc.Models.Identity;
using DemoMvc.Services.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoMvc.Components
{
    public class UserInfo : ViewComponent
    {
        IUserService userService;

        public UserInfo(IUserService userService)
        {
            this.userService = userService;
        }

        // Must have a method named Invoke OR InvokeAsync
        public async Task<IViewComponentResult> InvokeAsync()
        {
            UserDto user = await userService.GetUser(UserClaimsPrincipal);
            return View(user);
        }
    }
}
