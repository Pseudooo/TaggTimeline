
using MediatR;
using TaggTime.Service.Commands;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;

namespace TaggTime.Service.Handlers;

public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Category>
{
    private readonly IBaseRepository<Category> _baseRepository;
    private readonly ITransactionWrapper _transactionWrapper;

    public CreateCategoryHandler(IBaseRepository<Category> baseRepository, ITransactionWrapper transactionWrapper)
    {
        _baseRepository = baseRepository;
        _transactionWrapper = transactionWrapper;
    }

    public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        await using var t = await _transactionWrapper.Begin();

        var toBeCreated = new Category()
        {
            Key = request.Key,
        };

        var created = await _baseRepository.AddItem(toBeCreated);

        await t.Commit();

        return created;
    }
}
