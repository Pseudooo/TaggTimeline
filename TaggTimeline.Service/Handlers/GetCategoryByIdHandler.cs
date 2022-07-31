
using MapsterMapper;
using MediatR;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;
using TaggTimeline.Service.Exceptions;
using TaggTimeline.Service.Queries;

namespace TaggTime.Service.Handlers;

public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, CategoryModel>
{
    private readonly IBaseRepository<Category> _baseRepository;
    private readonly IMapper _mapper;

    public GetCategoryByIdHandler(IBaseRepository<Category> baseRepository, IMapper mapper)
    {
        _baseRepository = baseRepository;
        _mapper = mapper;
    }

    public async Task<CategoryModel> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _baseRepository.GetByIdWithNavigationProperties(request.Id, x => x.Taggs);

        if(category is null || category.UserId != request.UserId)
            throw new EntityNotFoundException($"Couldn't find Category with id:{request.Id}");

        var categoryModel = _mapper.Map<CategoryModel>(category);

        return categoryModel;
    }
}
