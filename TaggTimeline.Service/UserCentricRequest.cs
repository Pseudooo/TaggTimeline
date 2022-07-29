
using MediatR;

namespace TaggTimeline.Service;

public class UserCentricRequest<TReturn> : IRequest<TReturn>
{
    public string UserId { get; set; } = null!;
}
