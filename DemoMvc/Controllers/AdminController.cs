using DemoMvc.Models;
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

        public async Task<IActionResult> Index()
        {
            int catCount = await dashboard.GetCategoryCount();
            int prodCount = await dashboard.GetProductCount();
            int orderCount = await dashboard.GetPendingOrderCount();

            var model = new AdminIndexViewModel
            {
                CategoryCount = catCount,
                ProductCount = prodCount,
                OrderCount = orderCount,
            };

            return View(model);
        }
    }
}
