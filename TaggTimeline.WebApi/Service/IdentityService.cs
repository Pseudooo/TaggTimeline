
using Microsoft.AspNetCore.Identity;

namespace TaggTimeline.WebApi.Service;

public class IdentityService : IIdentityService
{
    private readonly UserManager<IdentityUser> _userManager;

    public IdentityService(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<AuthenticationResult> Register(string email, string password)
    {
        var existingUser = await _userManager.FindByEmailAsync(email);
        if(existingUser is not null)
            return new AuthenticationResult()
                {
                    Errors = new[] { "User with this email already exists" },
                };

        var newUser = new IdentityUser()
        {

        };

        throw new NotImplementedException();
    }
}
