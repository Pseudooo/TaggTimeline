
using MediatR;
using TaggTimeline.Domain.Entities.Taggs;

namespace TaggTimeline.Service.Commands;

public class CreateInstanceCommand : IRequest<Instance>
{
    public Guid TaggId { get; set; }
}
