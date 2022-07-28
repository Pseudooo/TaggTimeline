
namespace TaggTimeline.WebApi;

public static class Extensions
{
    public static string GetUserId(this HttpContext context)
    {
        if(context.User is null)
            return string.Empty;
        
        return context.User.Claims.Single(x => x.Type == "id").Value;
    }
}
