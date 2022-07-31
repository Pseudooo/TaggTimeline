
using MediatR;
using System.ComponentModel.DataAnnotations;
using TaggTimeline.ClientModel.Taggs;

namespace TaggTimeline.Service.Commands;

public class CreateTaggCommand : UserCentricRequest<TaggModel>
{
    [Required]
    public string Key { get; set; } = null!;
}
