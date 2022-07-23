
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using TaggTimeline.ClientModel.Auth;
using TaggTimeline.Service.Configuration;
using TaggTimeline.Service.Interface;

namespace TaggTimeline.Service.Service;

public class IdentityService : IIdentityService
{
    private readonly JwtConfiguration _jwtConfiguration;
    private readonly UserManager<IdentityUser> _userManager;

    public IdentityService(JwtConfiguration jwtConfiguration, UserManager<IdentityUser> userManager)
    {
        _jwtConfiguration = jwtConfiguration;
        _userManager = userManager;
    }

    public async Task<AuthenticationResultModel> Login(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);
        if(user is null)
            throw new Exception("..."); // Temporary exception

        var validPassword = await _userManager.CheckPasswordAsync(user, password);
        if(!validPassword)
            throw new Exception("..."); // Temporary exception

        return GenerateAuthenticationResultForUser(user);
    }

    public async Task<AuthenticationResultModel> Register(string username, string password)
    {
        var existingUser = await _userManager.FindByNameAsync(username);
        if(existingUser is not null)
            throw new Exception("..."); // Temporary exception

        var createdUser = new IdentityUser()
        {
            UserName = username,
        };

        var userCreationResult = await _userManager.CreateAsync(createdUser, password);
        if(!userCreationResult.Succeeded)
            throw new Exception("..."); // Temporary exception

        return GenerateAuthenticationResultForUser(createdUser);
    }

    private AuthenticationResultModel GenerateAuthenticationResultForUser(IdentityUser user)
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

        return new AuthenticationResultModel()
        {
            Token = tokenHandler.WriteToken(token),
        };
    }
}
