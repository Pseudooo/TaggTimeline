
using System.ComponentModel.DataAnnotations;
using MediatR;
using TaggTimeline.Domain.Entities.Taggs;

namespace TaggTime.Service.Queries;

public class SearchForCategoriesQuery : IRequest<IEnumerable<Category>>
{
    [Required]
    public string SearchTerm { get; set; } = null!;
}
