using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPT.Models;

namespace TestAPT.Interfaces
{
    public interface IProcessor
    {
        Task<FileUploaded> ProcessFile(IWebHostEnvironment host, IFormFile file);
    }
}
