using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using VZAggregator.Interfaces;

namespace api.Helpers
{
    public class LogUserActivity: IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
          var resultContext = await next();

          if(!resultContext.HttpContext.User.Identity.IsAuthenticated) return;
          var userId = resultContext.HttpContext.User.GetUserId();
          var usersRepository = context.HttpContext.RequestServices.GetRequiredService<IUsersRepository>();
          var user = await usersRepository.GetUserAsync(userId);
          // user.LastActive = DateTime.UtcNow;
        }
    }
}