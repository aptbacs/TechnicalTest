using TestAPT.Resources;

namespace TestAPT.Validators
{
    public class DetailValidationResult
    {
        public string TransactionCode { get; set; }
        public ErrorResponseResource Errors { get; set; }
        public int LineNumber { get; set; }
    }
}
