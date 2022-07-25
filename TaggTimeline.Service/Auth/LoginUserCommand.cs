
using MediatR;
using TaggTimeline.ClientModel.Auth;

namespace TaggTimeline.Service.Auth;

public class LoginUserCommand : IRequest<AuthenticationResultModel>
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
}
