using DemoMvc.Models;
using Microsoft.AspNetCore.Identity;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Have to keep this to do all the Identity stuff!
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>()
                .HasData(
                    CreateRole("Administrator"),
                    CreateRole("Editor")
                );

        }

        IdentityRole CreateRole(string roleName)
        {
            return new IdentityRole
            {
                Id = roleName,
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
                ConcurrencyStamp = "0",
            };
        }
    }
}
