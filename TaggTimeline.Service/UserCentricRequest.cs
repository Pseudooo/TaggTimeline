
using System.Text.Json.Serialization;
using MediatR;

namespace TaggTimeline.Service;

public class UserCentricRequest<TReturn> : IRequest<TReturn>
{
    [JsonIgnore]
    public string UserId { get; set; } = null!;
}
