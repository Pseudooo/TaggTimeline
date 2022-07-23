
using MediatR;
using TaggTimeline.Domain.Entities.Taggs;
using System.ComponentModel.DataAnnotations;

namespace TaggTimeline.Service.Commands;

public class CreateInstanceCommand : IRequest<Instance>
{
    [Required]
    public Guid TaggId { get; set; }

    [Required]
    public DateTime OccuranceDate { get; set; }
}
