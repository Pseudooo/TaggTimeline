
using MapsterMapper;
using MediatR;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;
using TaggTimeline.Service.Exceptions;
using TaggTimeline.Service.Queries;

namespace TaggTimeline.Service.Handlers;

public class GetTaggByIdHandler : IRequestHandler<GetTaggByIdQuery, TaggModel>
{

    private readonly IBaseRepository<Tagg> _baseRepository;
    private readonly IMapper _mapper;

    public GetTaggByIdHandler(IBaseRepository<Tagg> baseRepository, IMapper mapper)
    {
        _baseRepository = baseRepository;
        _mapper = mapper;
    }
    
    public async Task<TaggModel> Handle(GetTaggByIdQuery request, CancellationToken cancellationToken)
    {
        var tagg = await _baseRepository.GetByIdWithNavigationProperties(request.Id, x => x.Instances, x => x.Categories);
        
        if(tagg is null || tagg.UserId != request.UserId)
            throw new EntityNotFoundException($"Couldn't find Tagg with id:{request.Id}");

        var taggModel = _mapper.Map<TaggModel>(tagg);

        return taggModel; 
    }
}