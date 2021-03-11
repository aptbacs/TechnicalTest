using System.Collections.Generic;
using TestAPT.Validators;

namespace TestAPT.Models
{
    public class YieldResult
    {
        public FileUploaded File { get; set; }
        public List<DetailValidationResult> DetailsValidationResults { get; set; }
    }
}
