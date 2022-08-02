
using Microsoft.AspNetCore.Identity;
using TaggTimeline.ClientModel.Auth;

namespace TaggTimeline.Service.Interface;

public interface IIdentityService
{
    Task<AuthenticationResultModel> Register(string username, string password);
    Task<AuthenticationResultModel> Login(string username, string password);
    Task<IdentityUser?> GetIdentityUser(string username);
}
