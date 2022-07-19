
using MediatR;
using TaggTimeline.Domain.Entities.Taggs;

namespace TaggTimeline.Service.Commands;

public class CreateTaggCommand : IRequest<Tagg>
{
    public string Key { get; set; } = null!;
}
