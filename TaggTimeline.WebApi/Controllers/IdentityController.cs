
using Microsoft.AspNetCore.Mvc;
using TaggTimeline.WebApi.Service;

namespace TaggTimeline.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class IdentityController : ControllerBase
{
    private readonly IIdentityService _identityService;

    public IdentityController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
    {
        var authResponse = await _identityService.Register(request.Username, request.Password);

        if(!authResponse.Success)
        {
            return BadRequest(new AuthFailureResponse
            {
                Errors = authResponse.Errors,
            });
        }

        return Ok(new AuthSuccessResponse()
        {
            Token = authResponse.Token,
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
    {
        var authResponse = await _identityService.Login(request.Username, request.Password);

        if(!authResponse.Success)
        {
            return BadRequest(new AuthFailureResponse
            {
                Errors = authResponse.Errors,
            });
        }

        return Ok(new AuthSuccessResponse()
        {
            Token = authResponse.Token,
        });
    }

}
