
using MediatR;
using Microsoft.AspNetCore.Http;
using TaggTimeline.Service;

namespace TaggTImeline.Service.PipelineBehaviours;

public class UserRequestBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : UserCentricRequest<TResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserRequestBehaviour(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        request.UserId = _httpContextAccessor.HttpContext!.GetUserId();
        return next();
    }
}
