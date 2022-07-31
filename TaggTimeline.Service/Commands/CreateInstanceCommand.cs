
using MediatR;
using System.ComponentModel.DataAnnotations;
using TaggTimeline.ClientModel.Taggs;

namespace TaggTimeline.Service.Commands;

public class CreateInstanceCommand : UserCentricRequest<InstanceModel>
{
    [Required]
    public Guid TaggId { get; set; }

    [Required]
    public DateTime OccuranceDate { get; set; }
}
