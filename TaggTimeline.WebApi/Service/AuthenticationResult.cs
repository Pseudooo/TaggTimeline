
namespace TaggTimeline.WebApi.Service;

public class AuthenticationResult
{
    public string Token { get; set; }
    public string Success { get; set; }
    public IEnumerable<string> Errors { get; set;}
}
