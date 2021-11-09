using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DemoMvc.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        // If you leave this out, it will probably figure out that you need one
        [Display(Name = "Family")]
        public int FamilyId { get; set; }

        // Navigation Property that makes FamilyId a Foreign Key
        public Family Family { get; set; }
    }
}
