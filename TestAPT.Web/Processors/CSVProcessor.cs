using FluentValidation.Results;
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
using TestAPT.Resources;
using TestAPT.Validators;

namespace TestAPT.Processors
{
    public class CSVProcessor : IProcessor
    {
        public async Task<YieldResult> ProcessFile(IWebHostEnvironment host, IFormFile file)
        {
            var uploadPath = Path.Combine(host.WebRootPath, "FileBucket");
            if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);
            var fName = $"{Path.GetFileNameWithoutExtension(file.FileName)}-{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var fPath = Path.Combine(uploadPath, fName);
            using (var stream = new FileStream(fPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var content = File.ReadLines(fPath).ToList();
            //var contentToJson = CsvToJson(content); //Json Alternative
            var validationResults = new List<DetailValidationResult>();
            var details = new List<FileDetail>();
            foreach (var (line, arr, sb, d) in
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
            select (line, arr, sb, d))
            {
                for (int i = 3; i < arr.Length; i++)
                {
                    sb.Append($"{arr[i]}");
                }

                sb.Replace("£", "").Replace("$", "");
                var iAmount = Decimal.Parse(sb.Replace("\"", "").ToString(), NumberStyles.Currency, 
                    CultureInfo.InvariantCulture);

                d.Amount = iAmount;

                var validator = new FileDetailValidator();
                var ex = validator.Validate(d);
                if (ex.IsValid)
                {
                    details.Add(d);
                }
                else
                {
                    var err = new DetailValidationResult
                    {
                        TransactionCode = d.Code ?? "This line is missing a Transaction Code",
                        Errors = ValidationResultToErrorResponse(ex),
                        LineNumber = content.IndexOf(line) + 1
                    };
                    validationResults.Add(err);
                }
            }

            var fu = new FileUploaded()
            {
                Name = fName,
                TotalAmount = details.Sum(d => d.Amount),
                FileDetails = details
            };
            var result = new YieldResult {File = fu, DetailsValidationResults = validationResults };
            return result;
        }

        public static ErrorResponseResource ValidationResultToErrorResponse(ValidationResult result)
        {
            if (result == null || result.Errors == null
                || result.Errors.Count == 0) return null;

            var errorResult = new ErrorResponseResource();
            foreach (var res in result.Errors)
            {
                var errorInfoModel = new ErrorInfoModel
                {
                    FieldName = res.PropertyName,
                    Message = res.ErrorMessage
                };
                errorResult.Errors.Add(errorInfoModel);
            }
            return errorResult;
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
