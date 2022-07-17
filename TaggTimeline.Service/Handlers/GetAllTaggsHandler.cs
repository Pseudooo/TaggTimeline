
using MediatR;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Domain.Interface;
using TaggTimeline.Service.Queries;

namespace TaggTimeline.Service.Handlers;

public class GetAllTagsHandler : IRequestHandler<GetAllTagsQuery, IEnumerable<Tagg>>
{
    private readonly IBaseRepository<Tagg> _baseRepository;

    public GetAllTagsHandler(IBaseRepository<Tagg> baseRepository)
    {
        _baseRepository = baseRepository;
    }

    public async Task<IEnumerable<Tagg>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
    {
        var taggs = await _baseRepository.GetAll();
        return taggs;
    }
}
