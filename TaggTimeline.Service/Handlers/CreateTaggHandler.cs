
using MapsterMapper;
using MediatR;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;
using TaggTimeline.Service.Commands;

namespace TaggTimeline.Service.Handlers;

public class CreateTaggHandler : IRequestHandler<CreateTaggCommand, TaggModel>
{
    private readonly IBaseRepository<Tagg> _baseRepository;

    private readonly ITransactionWrapper _transactionWrapper;

    private readonly IMapper _mapper;

    public CreateTaggHandler(IBaseRepository<Tagg> baseRepository, ITransactionWrapper transactionWrapper, IMapper mapper)
    {
        _baseRepository = baseRepository;
        _transactionWrapper = transactionWrapper;
        _mapper = mapper;
    }

    public async Task<TaggModel> Handle(CreateTaggCommand request, CancellationToken cancellationToken)
    {
        await using var t = await _transactionWrapper.Begin();
        
        var toBeCreated = new Tagg()
        {
            Key = request.Key,
        };

        var createdTagg = await _baseRepository.AddItem(toBeCreated);

        var createdTaggModel = _mapper.Map<TaggModel>(createdTagg);
        
        await t.Commit();

        return createdTaggModel;
    }
}
