
using MapsterMapper;
using MediatR;
using TaggTime.Service.Queries;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;

namespace TaggTimeline.Service.Handlers;

public class SearchForCategoriesHandler : IRequestHandler<SearchForCategoriesQuery, IEnumerable<CategoryPreviewModel>>
{
    private readonly IKeyedEntityRepository<Category> _keyedEntityRepository;
    private readonly IMapper _mapper;

    public SearchForCategoriesHandler(IKeyedEntityRepository<Category> keyedEntityRepository, IMapper mapper)
    {
        _keyedEntityRepository = keyedEntityRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryPreviewModel>> Handle(SearchForCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _keyedEntityRepository.SearchForKey(request.SearchTerm);

        var categoryPreviewModels = _mapper.Map<IEnumerable<CategoryPreviewModel>>(categories);

        return categoryPreviewModels;
    }
}
