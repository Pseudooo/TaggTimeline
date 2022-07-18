
using MediatR;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;

namespace TaggTimeline.Service.Queries;

public class SearchForTaggQuery : IRequest<IEnumerable<TaggPreviewModel>>
{
    public string SearchTerm { get; set; }
}
