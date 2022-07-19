
using MediatR;
using TaggTimeline.Domain.Entities.Taggs;
using System.ComponentModel.DataAnnotations;

namespace TaggTimeline.Service.Queries;

public class GetTaggByIdQuery : IRequest<Tagg>
{
    [Required]
    public Guid Id { get; set; }
}
