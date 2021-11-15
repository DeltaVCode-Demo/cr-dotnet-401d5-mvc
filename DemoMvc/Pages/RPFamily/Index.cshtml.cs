using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DemoMvc.Data;
using DemoMvc.Models;

namespace DemoMvc.Pages.RPFamily
{
    public class IndexModel : PageModel
    {
        private readonly DemoMvc.Data.DemoDbContext _context;

        public IndexModel(DemoMvc.Data.DemoDbContext context)
        {
            _context = context;
        }

        public IList<Family> Family { get;set; }

        public async Task OnGetAsync()
        {
            Family = await _context.Families.ToListAsync();
        }
    }
}
