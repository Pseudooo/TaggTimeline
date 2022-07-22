
using MapsterMapper;
using MediatR;
using TaggTime.Service.Queries;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;

namespace TaggTime.Service.Handlers;

public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryPreviewModel>>
{
    private readonly IBaseRepository<Category> _baseRepository;
    private readonly IMapper _mapper;

    public GetAllCategoriesHandler(IBaseRepository<Category> baseRepository, IMapper mapper)
    {
        _baseRepository = baseRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryPreviewModel>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _baseRepository.GetAll();

        var categoryPreviews = _mapper.Map<IEnumerable<CategoryPreviewModel>>(categories);

        return categoryPreviews;
    }
}
