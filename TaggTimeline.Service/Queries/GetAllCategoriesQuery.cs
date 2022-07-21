
using MediatR;
using TaggTimeline.Domain.Entities.Taggs;

namespace TaggTime.Service.Queries;

public class GetAllCategoriesQuery : IRequest<IEnumerable<Category>>
    { }
