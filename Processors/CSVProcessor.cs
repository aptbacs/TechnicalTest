using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAPT.Interfaces;
using TestAPT.Models;

namespace TestAPT.Processors
{
    public class CSVProcessor : IProcessor
    {
        public async Task<FileUploaded> ProcessFile(IWebHostEnvironment host, IFormFile file)
        {
            var uploadPath = Path.Combine(host.WebRootPath, "FileBucket");
            if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);
            var fName = $"{file.FileName}-{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}";
            var fPath = Path.Combine(uploadPath, fName);
            using (var stream = new FileStream(fPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var content = File.ReadLines(fPath).ToList();
            //var contentToJson = CsvToJson(content); //Json Alternative
            var details = new List<FileDetail>();
            foreach (var (arr, sb, d) in
            from line in content
            where content.IndexOf(line) > 0
            let arr = line.Split(',')
            let sb = new StringBuilder()
            let d = new FileDetail
            {
                Code = arr[0] ?? string.Empty,
                Name = arr[1] ?? string.Empty,
                Reference = arr[2] ?? string.Empty
            }
            select (arr, sb, d))
            {
                for (int i = 3; i < arr.Length; i++)
                {
                    sb.Append($"{arr[i]}");
                }

                sb.Replace("£", "").Replace("$", "");
                d.Amount = Decimal.Parse(sb.Replace("\"", "").ToString(), NumberStyles.Currency, 
                    CultureInfo.InvariantCulture);

                details.Add(d);
            }

            var fu = new FileUploaded()
            {
                Name = fName,
                TotalAmount = details.Sum(d => d.Amount),
                FileDetails = details
            };
            return fu;
        }

        public static IEnumerable<JObject> CsvToJson(IEnumerable<string> csvLines)
        {
            var csvLinesList = csvLines.ToList();

            var header = csvLinesList[0].Split(',');
            for (int i = 1; i < csvLinesList.Count; i++)
            {
                var thisLineSplit = csvLinesList[i].Split(',');
                var pairedWithHeader = header.Zip(thisLineSplit, (h, v) => new KeyValuePair<string, string>(h, v));

                yield return new JObject(pairedWithHeader.Select(j => new JProperty(j.Key, j.Value)));
            }
        }
    }
}
