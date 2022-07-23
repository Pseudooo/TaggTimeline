
using MapsterMapper;
using MediatR;
using TaggTime.Service.Queries;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;
using TaggTimeline.Service.Exceptions;

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
        var category = await _baseRepository.GetById(request.Id);

        if(category is null)
            throw new EntityNotFoundException($"Couldn't find Category with id:{request.Id}");

        var categoryModel = _mapper.Map<CategoryModel>(category);

        return categoryModel;
    }
}
