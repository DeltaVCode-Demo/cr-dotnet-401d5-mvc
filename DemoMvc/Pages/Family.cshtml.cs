using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoMvc.Models;
using DemoMvc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoMvc.Pages
{
    public class FamilyModel : PageModel
    {
        private readonly IFamilyRepository familyRepository;

        public FamilyModel(IFamilyRepository familyRepository)
        {
            this.familyRepository = familyRepository;
        }

        // Sorta view model-ish
        public Family Family { get; set; }

        // Sorta Controller Action-ish
        public async Task OnGetAsync(int? id)
        {
            Family = await familyRepository.GetById(id);
        }
    }
}
