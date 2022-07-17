
using MediatR;
using TaggTimeline.Domain.Entities.Taggs;

namespace TaggTimeline.Service.Queries;

public class SearchForTaggQuery : IRequest<IEnumerable<Tagg>>
{
    public string SearchTerm { get; set; }
}
