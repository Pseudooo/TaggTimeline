
namespace TaggTimeline.WebApi.Service;

public interface IIdentityService
{
    Task<AuthenticationResult> Register(string email, string password);
}
