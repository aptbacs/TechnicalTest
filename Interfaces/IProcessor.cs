using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using TestAPT.Models;

namespace TestAPT.Interfaces
{
    public interface IProcessor
    {
        Task<YieldResult> ProcessFile(IWebHostEnvironment host, IFormFile file);
    }
}
