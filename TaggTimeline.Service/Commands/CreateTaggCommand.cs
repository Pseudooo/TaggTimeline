
using MediatR;
using TaggTimeline.Domain.Entities.Taggs;
using System.ComponentModel.DataAnnotations;

namespace TaggTimeline.Service.Commands;

public class CreateTaggCommand : IRequest<Tagg>
{
    [Required]
    public string Key { get; set; } = null!;
}
