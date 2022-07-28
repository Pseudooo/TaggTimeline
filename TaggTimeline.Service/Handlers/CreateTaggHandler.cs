
using MapsterMapper;
using MediatR;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;
using TaggTimeline.Service.Commands;

namespace TaggTimeline.Service.Handlers;

public class CreateTaggHandler : IRequestHandler<CreateTaggCommand, TaggModel>
{
    private readonly IBaseRepository<Tagg> _baseRepository;
    private readonly IUserMappingRepository _userMappingRepository;
    private readonly IMapper _mapper;

    public CreateTaggHandler(IBaseRepository<Tagg> baseRepository, IUserMappingRepository userMappingRepository, IMapper mapper)
    {
        _baseRepository = baseRepository;
        _userMappingRepository = userMappingRepository;
        _mapper = mapper;
    }

    public async Task<TaggModel> Handle(CreateTaggCommand request, CancellationToken cancellationToken)
    {
        var userMapping = await _userMappingRepository.GetByKnownUserId(request.UserId!);

        var toBeCreated = new Tagg()
        {
            Key = request.Key,
            Instances = Enumerable.Empty<Instance>(),
            Categories = Enumerable.Empty<Category>(),
            UserMapping = userMapping,
        };

        var createdTagg = await _baseRepository.AddItem(toBeCreated);

        var createdTaggModel = _mapper.Map<TaggModel>(createdTagg);

        return createdTaggModel;
    }
}
