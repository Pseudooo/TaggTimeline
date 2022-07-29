using Microsoft.AspNetCore.Http;

namespace TaggTimeline.Service;

public static class HttpContextExtensions
{
    public static string GetUserId(this HttpContext context)
    {
        if(context.User is null)
            return string.Empty;
        
        return context.User.Claims.Single(x => x.Type == "id").Value;
    }
}
