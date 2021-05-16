using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Database;
using WebApi.Models.POCO;

namespace WebApi.Filters
{
    public class CustomAuthorizationFilter : IActionFilter
    {

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.RouteData.Values.Values.Contains("GetAll") && context.RouteData.Values.Values.Contains("Category")) { }
            else if (context.RouteData.Values.Values.Contains("GetAll") && context.RouteData.Values.Values.Contains("User")) { }
            else if (context.RouteData.Values.Values.Contains("Get") && context.RouteData.Values.Values.Contains("User")) { }
            else if (context.RouteData.Values.Values.Contains("AddUser") && context.RouteData.Values.Values.Contains("User")) { }
            else
            {
                var res = context.HttpContext.Request.Headers.TryGetValue("userID", out Microsoft.Extensions.Primitives.StringValues callerID);

                var dbContext = context.HttpContext.RequestServices.GetRequiredService<DatabaseContext>();

                var usersID = dbContext.Users.Select(u => u.UserID).AsEnumerable<int>();
                int callerIDInt = int.Parse(callerID.ToString());
                bool isValidUser = usersID.Contains(callerIDInt);

                if (!isValidUser)
                {
                    var response = new ObjectResult(new { message = "User Unauthorized" });
                    response.StatusCode = 400;
                    context.Result = response;

                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // do something after 
        }
    }
}