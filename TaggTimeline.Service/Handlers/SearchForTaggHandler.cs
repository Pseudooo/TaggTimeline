
using MapsterMapper;
using MediatR;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;
using TaggTimeline.Service.Queries;

namespace TaggTimeline.Service.Handlers;

public class SearchForTaggHandler : IRequestHandler<SearchForTaggQuery, IEnumerable<TaggPreviewModel>>
{
    private readonly IKeyedEntityRepository<Tagg> _taggRepository;
    private readonly IMapper _mapper;

    public SearchForTaggHandler(IKeyedEntityRepository<Tagg> taggRepository, IMapper mapper)
    {
        _taggRepository = taggRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TaggPreviewModel>> Handle(SearchForTaggQuery request, CancellationToken cancellationToken)
    {
        var taggs = await _taggRepository.SearchForKeyFromUser(request.SearchTerm, request.UserId);

        var taggPreviewModels = _mapper.Map<IEnumerable<TaggPreviewModel>>(taggs);

        return taggPreviewModels;
    }   
}
