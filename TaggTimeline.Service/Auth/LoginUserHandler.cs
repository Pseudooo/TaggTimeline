
using MediatR;
using TaggTimeline.ClientModel.Auth;
using TaggTimeline.Service.Interface;

namespace TaggTimeline.Service.Auth;

public class LoginUserHandler : IRequestHandler<LoginUserCommand, AuthenticationResultModel>
{

    private readonly IIdentityService _identityService;

    public LoginUserHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public Task<AuthenticationResultModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        return _identityService.Login(request.UserName, request.Password);
    }
}
