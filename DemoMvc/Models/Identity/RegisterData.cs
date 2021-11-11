using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DemoMvc.Models.Identity
{
    public class RegisterData
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool AcceptedTerms { get; set; }

        // Could allow these if we also add to ApplicationUser
        // FirstName, LastName
    }
}
