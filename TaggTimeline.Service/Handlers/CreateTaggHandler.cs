
using MediatR;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;
using TaggTimeline.Service.Commands;

namespace TaggTimeline.Service.Handlers;

public class CreateTaggHandler : IRequestHandler<CreateTaggCommand, Tagg>
{
    private readonly IBaseRepository<Tagg> _baseRepository;

    public CreateTaggHandler(IBaseRepository<Tagg> baseRepository)
    {
        _baseRepository = baseRepository;
    }

    public async Task<Tagg> Handle(CreateTaggCommand request, CancellationToken cancellationToken)
    {
        var toBeCreated = new Tagg()
        {
            Key = request.Key,
        };

        var created = await _baseRepository.AddItem(toBeCreated);
        return created;
    }
}
