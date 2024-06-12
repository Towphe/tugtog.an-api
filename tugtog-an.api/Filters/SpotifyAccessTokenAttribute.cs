using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace tugtog_an.api.Filters;

public class SpotifyAccessTokenAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(
        ActionExecutingContext context, ActionExecutionDelegate next)
    {   
        var accessToken = context.HttpContext.Request.Cookies["sat"];
        
        if (accessToken == null){
            // unauthenticated
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await context.HttpContext.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new { message = "Unauthenticated. Request for anonymous token first." })));
            return;
        }

        await next();
    }
}