using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPT.Interfaces;
using TestAPT.Models;

namespace TestAPT.Processors
{
    public class TextFileProcessor : IProcessor
    {
        public Task<FileUploaded> ProcessFile(IWebHostEnvironment host, IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
