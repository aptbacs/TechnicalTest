using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<FileResponseResource>> Post()
        {
            YieldResult result = null;
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
                _context.FileUploads.Add(result.File);

                await _context.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                if (result.DetailsValidationResults != null && result.DetailsValidationResults.Any())
                    Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(result.DetailsValidationResults));
                Console.WriteLine(ex.ToString());
                return new BadRequestObjectResult(ex.Message);
            }
            finally
            {
                if(result.DetailsValidationResults!=null && result.DetailsValidationResults.Any())
                Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(result.DetailsValidationResults));
            }
            //TODO - maybe AutoMapper is too over kill, Not in list of requirements
            return Ok(new FileResponseResource { FileName = result.File.Name,
                TotalLinesRead = result.File.FileDetails.Count,
                ErrorMessages = result.DetailsValidationResults});
        }
    }
}
