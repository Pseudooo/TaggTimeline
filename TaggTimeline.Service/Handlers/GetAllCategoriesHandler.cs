
using MediatR;
using TaggTime.Service.Queries;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;

namespace TaggTime.Service.Handlers;

public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<Category>>
{
    private readonly IBaseRepository<Category> _baseRepository;

    public GetAllCategoriesHandler(IBaseRepository<Category> baseRepository)
    {
        _baseRepository = baseRepository;
    }

    public async Task<IEnumerable<Category>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _baseRepository.GetAll();
        return categories;
    }
}
