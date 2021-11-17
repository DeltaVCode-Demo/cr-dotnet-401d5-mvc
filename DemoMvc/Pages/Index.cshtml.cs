using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DemoMvc.Models;
using DemoMvc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoMvc.Pages
{
    public class IndexModel : PageModel
    {
        IFamilyRepository familyRepository;

        public IndexModel(IFamilyRepository familyRepository)
        {
            this.familyRepository = familyRepository;
        }

        [DisplayFormat(DataFormatString = "{0:hh:mm:ss tt}")]
        public DateTime PageGeneratedAt { get; private set; }

        public List<Family> NewFamilies { get; private set; }

        public async Task OnGetAsync()
        {
            PageGeneratedAt = DateTime.Now;

            NewFamilies = await familyRepository.GetNew(4);
        }
    }
}
