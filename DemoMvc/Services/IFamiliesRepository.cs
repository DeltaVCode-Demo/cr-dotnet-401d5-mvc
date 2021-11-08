using DemoMvc.Models;
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
        public async Task<List<Family>> GetAll()
        {
            return new List<Family>
            {
                new Family { Id = 45, Name = "Jetsons" },
            };
        }
    }
}
