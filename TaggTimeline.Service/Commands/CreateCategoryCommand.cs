
using System.ComponentModel.DataAnnotations;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Service;

namespace TaggTime.Service.Commands;

public class CreateCategoryCommand : UserCentricRequest<CategoryModel>
{
    [Required]
    public string Key { get; set; } = null!;
}
