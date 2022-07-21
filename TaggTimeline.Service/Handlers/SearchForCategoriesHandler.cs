
using MediatR;
using TaggTime.Service.Queries;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;

namespace TaggTimeline.Service.Handlers;

public class SearchForCategoriesHandler : IRequestHandler<SearchForCategoriesQuery, IEnumerable<Category>>
{
    private readonly IKeyedEntityRepository<Category> _keyedEntityRepository;

    public SearchForCategoriesHandler(IKeyedEntityRepository<Category> keyedEntityRepository)
    {
        _keyedEntityRepository = keyedEntityRepository;
    }

    public async Task<IEnumerable<Category>> Handle(SearchForCategoriesQuery request, CancellationToken cancellationToken)
    {
        var result = await _keyedEntityRepository.SearchForKey(request.SearchTerm);
        return result;
    }
}
