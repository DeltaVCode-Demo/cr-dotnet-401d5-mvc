using DemoMvc.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoMvc.Data
{
    public class DemoDbContext : IdentityDbContext /* <ApplicationUser> */
    {
        public DemoDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Family> Families { get; set; }
        public DbSet<Person> Persons { get; set; }
    }
}
