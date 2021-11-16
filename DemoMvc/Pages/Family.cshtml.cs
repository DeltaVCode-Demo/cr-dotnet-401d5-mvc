using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoMvc.Pages
{
    public class FamilyModel : PageModel
    {
        public Family Family { get; set; }

        public void OnGet(int? id)
        {
        }
    }
}
