
using System.ComponentModel.DataAnnotations;
using TaggTimeline.ClientModel.Taggs;

namespace TaggTimeline.Service.Queries;

public class GetCategoryByIdQuery : UserCentricRequest<CategoryModel>
{
    [Required]
    public Guid Id { get; set; }
}
