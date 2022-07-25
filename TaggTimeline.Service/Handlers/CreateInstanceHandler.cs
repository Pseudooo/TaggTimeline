
using MapsterMapper;
using MediatR;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;
using TaggTimeline.Service.Commands;
using TaggTimeline.Service.Exceptions;

namespace TaggTimeline.Service.Handlers;

public class CreateInstanceHandler : IRequestHandler<CreateInstanceCommand, InstanceModel>
{
    private readonly IBaseRepository<Tagg> _baseRepository;
    private readonly IMapper _mapper;

    public CreateInstanceHandler(IBaseRepository<Tagg> baseRepository, IMapper mapper)
    {
        _baseRepository = baseRepository;
        _mapper = mapper;
    }

    public async Task<InstanceModel> Handle(CreateInstanceCommand request, CancellationToken cancellationToken)
    {
        var instance = new Instance() 
        {
            OccuranceDate = request.OccuranceDate,
        };

        var tagg = await _baseRepository.GetByIdWithNavigationProperties(request.TaggId, x => x.Instances);
        if(tagg is null)
            throw new EntityNotFoundException($"Couldn't find a Tagg with id:{request.TaggId}");

        tagg.Instances = tagg.Instances.Append(instance).ToList();

        await _baseRepository.SaveChanges(CancellationToken.None);

        var instanceModel = _mapper.Map<InstanceModel>(instance);

        return instanceModel;
    }
}
