
using MediatR;
using TaggTimeline.Domain.Entities.Taggs;

namespace TaggTimeline.Service.Queries;

public class GetAllTagsQuery : IRequest<IEnumerable<Tagg>>
    { }
