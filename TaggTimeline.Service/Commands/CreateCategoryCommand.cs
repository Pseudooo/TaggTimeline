
using System.ComponentModel.DataAnnotations;
using MediatR;
using TaggTimeline.ClientModel.Taggs;

namespace TaggTime.Service.Commands;

public class CreateCategoryCommand : IRequest<CategoryModel>
{
    [Required]
    public string Key { get; set; } = null!;
}
