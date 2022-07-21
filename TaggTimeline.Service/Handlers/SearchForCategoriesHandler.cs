
using MediatR;
using TaggTime.Service.Queries;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;

namespace TaggTimeline.Service.Handlers;

public class SearchForCategoriesHandler : IRequestHandler<SearchForCategoriesQuery, IEnumerable<CategoryPreviewModel>>
{
    private readonly IKeyedEntityRepository<Category> _keyedEntityRepository;

    public SearchForCategoriesHandler(IKeyedEntityRepository<Category> keyedEntityRepository)
    {
        _keyedEntityRepository = keyedEntityRepository;
    }

    public async Task<IEnumerable<CategoryPreviewModel>> Handle(SearchForCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _keyedEntityRepository.SearchForKey(request.SearchTerm);

        var categoryPreviews = categories.Select(category => new CategoryPreviewModel() 
        {
            Id = category.Id,
            Key = category.Key,
        }).ToList();

        return categoryPreviews;
    }
}
