
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
            Email = email,
            UserName = email,
        };

        var createdUser = await _userManager.CreateAsync(newUser, password);
        if(!createdUser.Succeeded)
            return new AuthenticationResult()
            {
                Errors = createdUser.Errors.Select(x => x.Description).ToList(),
            };

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtConfiguration.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new []
            {
                new Claim(JwtRegisteredClaimNames.Sub, newUser.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, newUser.Email),
                new Claim("id", newUser.Id),
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
