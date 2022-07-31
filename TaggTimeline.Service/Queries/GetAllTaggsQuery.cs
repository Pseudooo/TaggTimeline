
using MediatR;
using TaggTimeline.ClientModel.Taggs;

namespace TaggTimeline.Service.Queries;

public class GetAllTaggsQuery : UserCentricRequest<IEnumerable<TaggPreviewModel>>, IRequest<IEnumerable<TaggPreviewModel>>
    { }
