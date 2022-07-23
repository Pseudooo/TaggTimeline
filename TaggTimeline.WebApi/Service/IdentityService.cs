
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using TaggTimeline.WebApi.Configuration;

namespace TaggTimeline.WebApi.Service;

public class IdentityService : IIdentityService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtConfiguration _jwtConfiguration;

    public IdentityService(UserManager<IdentityUser> userManager, JwtConfiguration jwtConfiguration)
    {
        _userManager = userManager;
        _jwtConfiguration = jwtConfiguration;
    }

    public async Task<AuthenticationResult> Login(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);
        if(user is null)
            return new AuthenticationResult()
                {
                    Errors = new [] { "User doesn't exist" }
                };

        var isUserPasswordValid = await _userManager.CheckPasswordAsync(user, password);
        if(!isUserPasswordValid)
            return new AuthenticationResult()
                {
                    Errors = new[] { "Invalid username/password combination" }
                };

        return GenerateAuthenticationResultForUser(user);
    }

    public async Task<AuthenticationResult> Register(string username, string password)
    {
        var existingUser = await _userManager.FindByNameAsync(username);
        if(existingUser is not null)
            return new AuthenticationResult()
                {
                    Errors = new[] { "User with this name already exists" },
                };

        var newUser = new IdentityUser()
        {
            UserName = username,
        };

        var createdUser = await _userManager.CreateAsync(newUser, password);
        if(!createdUser.Succeeded)
            return new AuthenticationResult()
            {
                Errors = createdUser.Errors.Select(x => x.Description).ToList(),
            };

        return GenerateAuthenticationResultForUser(newUser);
    }

    private AuthenticationResult GenerateAuthenticationResultForUser(IdentityUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtConfiguration.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new []
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.UserName),
                new Claim("id", user.Id),
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new AuthenticationResult()
        {
            Success = true,
            Token = tokenHandler.WriteToken(token),
        };
    }
}
