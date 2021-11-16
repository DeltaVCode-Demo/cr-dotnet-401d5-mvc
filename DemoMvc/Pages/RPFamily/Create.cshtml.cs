using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DemoMvc.Data;
using DemoMvc.Models;

namespace DemoMvc.Pages.RPFamily
{
    public class CreateModel : PageModel
    {
        private readonly DemoMvc.Data.DemoDbContext _context;

        public CreateModel(DemoMvc.Data.DemoDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Family Family { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Families.Add(Family);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
