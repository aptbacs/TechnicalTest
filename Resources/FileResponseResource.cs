using System.Collections.Generic;
using TestAPT.Validators;

namespace TestAPT.Resources
{
    public class FileResponseResource
    {
        public string FileName { get; set; }
        public int TotalLinesRead { get; set; }
        public List<DetailValidationResult> ErrorMessages { get; set; }
    }
}
