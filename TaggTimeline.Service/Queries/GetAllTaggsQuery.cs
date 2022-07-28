
using MediatR;
using TaggTimeline.ClientModel.Taggs;

namespace TaggTimeline.Service.Queries;

public class GetAllTaggsQuery : IRequest<IEnumerable<TaggPreviewModel>>
{
    public string UserId { get; set; } = null!;
}
