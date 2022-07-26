
using MapsterMapper;
using MediatR;
using TaggTime.Service.Commands;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;

namespace TaggTime.Service.Handlers;

public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, CategoryModel>
{
    private readonly IBaseRepository<Category> _baseRepository;
    private readonly IMapper _mapper;

    public CreateCategoryHandler(IBaseRepository<Category> baseRepository, IMapper mapper)
    {
        _baseRepository = baseRepository;
        _mapper = mapper;
    }

    public async Task<CategoryModel> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var toBeCreated = new Category()
        {
            Key = request.Key,
            Taggs = Enumerable.Empty<Tagg>(),
        };

        var created = await _baseRepository.AddItem(toBeCreated);

        var createdModel = _mapper.Map<CategoryModel>(created);

        return createdModel;
    }
}
