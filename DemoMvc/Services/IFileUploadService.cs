using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoMvc.Services
{
    public interface IFileUploadService
    {
        Task<string> Upload(IFormFile file);
    }

    public class KeithFileUploadService : IFileUploadService
    {
        public async Task<string> Upload(IFormFile file)
        {
            return "https://avatars.githubusercontent.com/u/133987?v=4";
        }
    }
}
