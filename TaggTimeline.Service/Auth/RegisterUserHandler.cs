
using MediatR;
using TaggTimeline.ClientModel.Auth;
using TaggTimeline.Service.Interface;

namespace TaggTimeline.Service.Auth;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, AuthenticationResultModel>
{

    private readonly IIdentityService _identityService;

    public RegisterUserHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public Task<AuthenticationResultModel> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        return _identityService.Register(request.UserName, request.Password);
    }
}
