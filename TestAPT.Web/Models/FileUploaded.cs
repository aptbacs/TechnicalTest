using System;
using System.Collections.Generic;

namespace TestAPT.Models
{
    public class FileUploaded
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime TimeStamp { get; set; }
        public virtual IList<FileDetail> FileDetails { get; set; }
    }
}
