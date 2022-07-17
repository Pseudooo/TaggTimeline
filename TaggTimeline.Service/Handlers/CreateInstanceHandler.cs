
using MediatR;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;
using TaggTimeline.Service.Commands;
using TaggTimeline.Service.Exceptions;

namespace TaggTimeline.Service.Handlers;

public class CreateInstanceHandler : IRequestHandler<CreateInstanceCommand, Instance>
{
    private readonly IBaseRepository<Tagg> _baseRepository;

    public CreateInstanceHandler(IBaseRepository<Tagg> baseRepository)
    {
        _baseRepository = baseRepository;
    }

    public async Task<Instance> Handle(CreateInstanceCommand request, CancellationToken cancellationToken)
    {
        var instance = new Instance();

        var tagg = await _baseRepository.GetByIdWithNavigationProperties(request.TaggId, x => x.Instances);
        if(tagg is null)
            throw new EntityNotFoundException($"Couldn't find a Tagg with id:{request.TaggId}");

        tagg.Instances = tagg.Instances.Append(instance).ToList();

        await _baseRepository.SaveChanges(CancellationToken.None);

        return instance;
    }
}
