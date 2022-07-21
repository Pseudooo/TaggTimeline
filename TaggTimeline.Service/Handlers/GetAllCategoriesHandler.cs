
using MediatR;
using TaggTime.Service.Queries;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;

namespace TaggTime.Service.Handlers;

public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryPreviewModel>>
{
    private readonly IBaseRepository<Category> _baseRepository;

    public GetAllCategoriesHandler(IBaseRepository<Category> baseRepository)
    {
        _baseRepository = baseRepository;
    }

    public async Task<IEnumerable<CategoryPreviewModel>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _baseRepository.GetAll();

        var categoryPreviews = categories.Select(category => new CategoryPreviewModel() 
        {
            Id = category.Id,
            Key = category.Key,    
        }).ToList();

        return categoryPreviews;
    }
}
