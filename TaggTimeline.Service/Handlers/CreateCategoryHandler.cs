
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
    private readonly ITransactionWrapper _transactionWrapper;
    private readonly IMapper _mapper;

    public CreateCategoryHandler(IBaseRepository<Category> baseRepository, ITransactionWrapper transactionWrapper, IMapper mapper)
    {
        _baseRepository = baseRepository;
        _transactionWrapper = transactionWrapper;
        _mapper = mapper;
    }

    public async Task<CategoryModel> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        await using var t = await _transactionWrapper.Begin();

        var toBeCreated = new Category()
        {
            Key = request.Key,
        };

        var created = await _baseRepository.AddItem(toBeCreated);

        await t.Commit();

        var createdModel = _mapper.Map<CategoryModel>(created);

        return createdModel;
    }
}
