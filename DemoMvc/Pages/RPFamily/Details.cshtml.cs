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
    public class DetailsModel : PageModel
    {
        private readonly DemoMvc.Data.DemoDbContext _context;

        public DetailsModel(DemoMvc.Data.DemoDbContext context)
        {
            _context = context;
        }

        public Family Family { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Family = await _context.Families.FirstOrDefaultAsync(m => m.Id == id);

            if (Family == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
