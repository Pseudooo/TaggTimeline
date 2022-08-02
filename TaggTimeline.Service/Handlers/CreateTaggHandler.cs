
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
    private readonly IMapper _mapper;

    public CreateTaggHandler(IBaseRepository<Tagg> baseRepository, IMapper mapper)
    {
        _baseRepository = baseRepository;
        _mapper = mapper;
    }

    public async Task<TaggModel> Handle(CreateTaggCommand request, CancellationToken cancellationToken)
    {
        var toBeCreated = new Tagg()
        {
            Key = request.Key,
            Instances = Enumerable.Empty<Instance>(),
            Categories = Enumerable.Empty<Category>(),
            UserId = request.UserId!,
        };

        var createdTagg = await _baseRepository.AddItem(toBeCreated);

        var createdTaggModel = _mapper.Map<TaggModel>(createdTagg);

        return createdTaggModel;
    }
}
