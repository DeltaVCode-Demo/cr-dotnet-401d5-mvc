using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoMvc.Components
{
    public class UserInfo : ViewComponent
    {
        // Must have a method named Invoke OR InvokeAsync
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
