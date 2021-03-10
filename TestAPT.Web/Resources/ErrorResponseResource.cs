using System.Collections.Generic;

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
