
using MediatR;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;
using TaggTimeline.Service.Queries;

namespace TaggTimeline.Service.Handlers;

public class SearchForTaggHandler : IRequestHandler<SearchForTaggQuery, IEnumerable<TaggPreviewModel>>
{
    private readonly ITaggRepository _taggRepository;

    public SearchForTaggHandler(ITaggRepository taggRepository)
    {
        _taggRepository = taggRepository;
    }

    public async Task<IEnumerable<TaggPreviewModel>> Handle(SearchForTaggQuery request, CancellationToken cancellationToken)
    {
        var taggs = await _taggRepository.SearchForTagg(request.SearchTerm);

        var taggPreviews = taggs.Select(tagg => new TaggPreviewModel() { Id = tagg.Id, Key = tagg.Key })
                                .ToList();

        return taggPreviews;
    }   
}
