using System.Linq;
using GeoToast.Infrastructure.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GeoToast.Infrastructure.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.ModelState.AddModelError("", "This is a model wide error");

                context.Result = new ValidationFailedResult(context.ModelState);
            }
        }
    }
}