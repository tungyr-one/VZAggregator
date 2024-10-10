using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace api.Helpers
{
    public class LogUserActivity: IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
      {
         var resultContext = await next();

         if(!resultContext.HttpContext.User.Identity.IsAuthenticated) return;
         var userId = resultContext.HttpContext.User.GetUserId();
      }
    }
}