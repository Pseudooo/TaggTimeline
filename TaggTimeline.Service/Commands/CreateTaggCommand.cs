
using MediatR;
using System.ComponentModel.DataAnnotations;
using TaggTimeline.ClientModel.Taggs;

namespace TaggTimeline.Service.Commands;

public class CreateTaggCommand : IRequest<TaggModel>
{
    [Required]
    public string Key { get; set; } = null!;

    [Required]
    public string Colour { get; set; } = null!;
}
