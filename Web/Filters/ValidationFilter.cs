using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;
using TestAPT.Resources;

namespace TestAPT.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .ToDictionary(v => v.Key, v => v.Value.Errors.Select(e => e.ErrorMessage))
                    .ToArray();

                var errorResponse = new ErrorResponseResource();
                foreach (var error in errors)
                {
                    foreach (var subError in error.Value)
                    {
                        var errorInfoModel = new ErrorInfoModel
                        {
                            FieldName = error.Key,
                            Message = subError
                        };
                        errorResponse.Errors.Add(errorInfoModel);
                    }
                }

                context.Result = new BadRequestObjectResult(errorResponse);
                return;
            }
            await next();
        }
    }
}
