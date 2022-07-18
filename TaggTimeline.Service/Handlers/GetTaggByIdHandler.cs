
using MediatR;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;
using TaggTimeline.Service.Exceptions;
using TaggTimeline.Service.Queries;

namespace TaggTimeline.Service.Handlers;

public class GetTaggByIdHandler : IRequestHandler<GetTaggByIdQuery, Tagg>
{

    private readonly IBaseRepository<Tagg> _baseRepository;

    public GetTaggByIdHandler(IBaseRepository<Tagg> baseRepository)
    {
        _baseRepository = baseRepository;
    }
    
    public async Task<Tagg> Handle(GetTaggByIdQuery request, CancellationToken cancellationToken)
    {
        var tagg = await _baseRepository.GetByIdWithNavigationProperties(request.Id, x => x.Instances);

        if(tagg is null)
            throw new EntityNotFoundException($"Couldn't find Tagg with id:{request.Id}");

        return tagg; 
    }
}