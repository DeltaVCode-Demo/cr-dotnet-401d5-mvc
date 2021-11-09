using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoMvc.Services
{
    public interface IDashboardRepository
    {
        Task<int> GetCategoryCount();
        Task<int> GetProductCount();
        Task<int> GetPendingOrderCount();
    }

    public class DashboardRepository : IDashboardRepository
    {
        // TODO: Actually go to database or Shopify API

        public async Task<int> GetCategoryCount()
        {
            // return await _context.Categories.CountAsync();
            return 12;
        }

        public async Task<int> GetPendingOrderCount()
        {
            return (int)DateTime.Today.DayOfWeek;
        }

        public async Task<int> GetProductCount()
        {
            return 98;
        }
    }
}
