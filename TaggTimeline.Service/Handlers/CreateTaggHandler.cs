
using MediatR;
using TaggTimeline.Domain;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;
using TaggTimeline.Service.Commands;

namespace TaggTimeline.Service.Handlers;

public class CreateTaggHandler : IRequestHandler<CreateTaggCommand, Tagg>
{
    private readonly IBaseRepository<Tagg> _baseRepository;

    private readonly ITransactionWrapper _transactionWrapper;

    public CreateTaggHandler(IBaseRepository<Tagg> baseRepository, ITransactionWrapper transactionWrapper)
    {
        _baseRepository = baseRepository;
        _transactionWrapper = transactionWrapper;
    }

    public async Task<Tagg> Handle(CreateTaggCommand request, CancellationToken cancellationToken)
    {
        await using var t = await _transactionWrapper.Begin();
        
        var toBeCreated = new Tagg()
        {
            Key = request.Key,
        };

        var created = await _baseRepository.AddItem(toBeCreated);
        
        await t.Commit();

        return created;
    }
}
