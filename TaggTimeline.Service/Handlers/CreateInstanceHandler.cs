
using MediatR;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;
using TaggTimeline.Service.Commands;
using TaggTimeline.Service.Exceptions;

namespace TaggTimeline.Service.Handlers;

public class CreateInstanceHandler : IRequestHandler<CreateInstanceCommand, Instance>
{
    private readonly IBaseRepository<Tagg> _baseRepository;
    private readonly ITransactionWrapper _transactionWrapper;

    public CreateInstanceHandler(IBaseRepository<Tagg> baseRepository, ITransactionWrapper transactionWrapper)
    {
        _baseRepository = baseRepository;
        _transactionWrapper = transactionWrapper;
    }

    public async Task<Instance> Handle(CreateInstanceCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await _transactionWrapper.Begin();

        var instance = new Instance();

        var tagg = await _baseRepository.GetByIdWithNavigationProperties(request.TaggId, x => x.Instances);
        if(tagg is null)
            throw new EntityNotFoundException($"Couldn't find a Tagg with id:{request.TaggId}");

        tagg.Instances = tagg.Instances.Append(instance).ToList();

        await _baseRepository.SaveChanges(CancellationToken.None);

        await transaction.Commit();

        return instance;
    }
}
