using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoMvc.Models
{
    public class AdminIndexViewModel
    {
        public int CategoryCount { get; set; }
        // public List<Category> TopCategories { get; set; }

        public int ProductCount { get; set; }
        public int OrderCount { get; set; }
    }
}
