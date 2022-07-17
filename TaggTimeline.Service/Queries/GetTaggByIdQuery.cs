
using MediatR;
using TaggTimeline.Domain.Entities.Taggs;

namespace TaggTimeline.Service.Queries;

public class GetTaggByIdQuery : IRequest<Tagg>
{
    public Guid Id { get; set; }
}
