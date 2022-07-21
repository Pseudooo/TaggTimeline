
using System.ComponentModel.DataAnnotations;
using MediatR;
using TaggTimeline.ClientModel.Taggs;

namespace TaggTime.Service.Queries;

public class SearchForCategoriesQuery : IRequest<IEnumerable<CategoryPreviewModel>>
{
    [Required]
    public string SearchTerm { get; set; } = null!;
}
