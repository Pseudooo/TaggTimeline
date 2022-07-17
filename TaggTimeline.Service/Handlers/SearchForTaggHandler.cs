
using MediatR;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;
using TaggTimeline.Service.Queries;

namespace TaggTimeline.Service.Handlers;

public class SearchForTaggHandler : IRequestHandler<SearchForTaggQuery, IEnumerable<Tagg>>
{
    private readonly ITaggRepository _taggRepository;

    public SearchForTaggHandler(ITaggRepository taggRepository)
    {
        _taggRepository = taggRepository;
    }

    public async Task<IEnumerable<Tagg>> Handle(SearchForTaggQuery request, CancellationToken cancellationToken)
    {
        var result = await _taggRepository.SearchForTagg(request.SearchTerm);
        return result;
    }   
}
