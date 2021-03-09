using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAPT.Resources
{
    /// <summary>
    /// Returns all the errors for the client
    /// </summary>
    public class ErrorResponseResource
    {
        public List<ErrorInfoModel> Errors { get; set; } = new List<ErrorInfoModel>();
    }
}
