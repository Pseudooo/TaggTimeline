
using System.ComponentModel.DataAnnotations;
using MediatR;
using TaggTimeline.Domain.Entities.Taggs;

namespace TaggTime.Service.Commands;

public class CreateCategoryCommand : IRequest<Category>
{
    [Required]
    public string Key { get; set; } = null!;
}
