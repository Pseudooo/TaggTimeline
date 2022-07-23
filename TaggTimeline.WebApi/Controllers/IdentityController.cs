
using Microsoft.AspNetCore.Mvc;
using TaggTimeline.WebApi.Service;

namespace TaggTimeline.WebApi.Controllers;

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

        var authResponse = await _identityService.Register(request.Email, request.Password);

        return Ok();
    }

}
