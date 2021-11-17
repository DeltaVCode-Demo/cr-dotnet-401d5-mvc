using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DemoMvc.Data;
using DemoMvc.Models;

namespace DemoMvc.Pages
{
    public class TestingModel : PageModel
    {
        private readonly DemoMvc.Data.DemoDbContext _context;

        public TestingModel(DemoMvc.Data.DemoDbContext context)
        {
            _context = context;
        }

        public Person Person { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Person = await _context.Persons
                .Include(p => p.Family).FirstOrDefaultAsync(m => m.Id == id);

            if (Person == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
