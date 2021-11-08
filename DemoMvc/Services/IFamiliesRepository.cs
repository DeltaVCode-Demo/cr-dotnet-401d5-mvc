using DemoMvc.Data;
using DemoMvc.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoMvc.Services
{
    public interface IFamilyRepository
    {
        Task<List<Family>> GetAll();
    }

    public class DatabaseFamilyRepository : IFamilyRepository
    {
        private readonly DemoDbContext _context;

        public DatabaseFamilyRepository(DemoDbContext context)
        {
            _context = context;
        }

        public async Task<List<Family>> GetAll()
        {
            //return new List<Family>
            //{
            //    new Family { Id = 45, Name = "Jetsons" },
            //};
            return await _context.Families.ToListAsync();
        }
    }
}
