
using MediatR;
using TaggTimeline.Domain;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;
using TaggTimeline.Service.Commands;

namespace TaggTimeline.Service.Handlers;

public class CreateTaggHandler : IRequestHandler<CreateTaggCommand, Tagg>
{
    private readonly IBaseRepository<Tagg> _baseRepository;

    private readonly ITransaction _transaction;

    public CreateTaggHandler(IBaseRepository<Tagg> baseRepository, ITransaction transaction)
    {
        _baseRepository = baseRepository;
        _transaction = transaction;
    }

    public async Task<Tagg> Handle(CreateTaggCommand request, CancellationToken cancellationToken)
    {
        using var trans = _transaction.InitialiseTransaction();
        
        var toBeCreated = new Tagg()
        {
            Key = request.Key,
        };

        var created = await _baseRepository.AddItem(toBeCreated);
        
        return created;
    }
}
