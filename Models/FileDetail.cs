namespace TestAPT.Models
{
    public class FileDetail
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Reference { get; set; }
        public decimal Amount { get; set; }
        public FileUploaded File { get; set; }
    }
}
