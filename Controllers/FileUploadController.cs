using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAPT.Interfaces;
using TestAPT.Models;
using TestAPT.Processors;
using TestAPT.Resources;

namespace TestAPT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly IWebHostEnvironment _host;

        public FileUploadController(MyDbContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult<FileResponseResource>> post()
        {
            FileUploaded result = null;
            try
            {
                var file = Request.Form.Files[0];
                var ext = Path.GetExtension(file.FileName);
                switch (ext)
                {
                    case ".txt":
                        result = await new TextFileProcessor().ProcessFile(_host, file);
                        break;
                    case ".csv":
                        result = await new CSVProcessor().ProcessFile(_host, file);
                        break;
                    default:
                        return new UnsupportedMediaTypeResult();
                }
                _context.FileUploads.Add(result);

                await _context.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            //TODO - maybe AutoMapper is too over kill, Not in list of requirements
            return Ok(new FileResponseResource { FileName = result.Name, 
                TotalLinesRead = result.FileDetails.Count });
        }
    }
}
