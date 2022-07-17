
using MediatR;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;
using TaggTimeline.Service.Queries;

namespace TaggTimeline.Service.Handlers;

public class GetTaggByIdHandler : IRequestHandler<GetTaggByIdQuery, Tagg?>
{

    private readonly IBaseRepository<Tagg> _baseRepository;

    public GetTaggByIdHandler(IBaseRepository<Tagg> baseRepository)
    {
        _baseRepository = baseRepository;
    }
    
    public Task<Tagg?> Handle(GetTaggByIdQuery request, CancellationToken cancellationToken)
    {
        return _baseRepository.GetByIdWithNavigationProperties(request.Id, x => x.Instances);
    }
}