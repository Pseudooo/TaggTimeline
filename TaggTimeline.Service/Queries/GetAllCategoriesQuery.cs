
using MediatR;
using TaggTimeline.ClientModel.Taggs;

namespace TaggTime.Service.Queries;

public class GetAllCategoriesQuery : IRequest<IEnumerable<CategoryPreviewModel>>
    { }
