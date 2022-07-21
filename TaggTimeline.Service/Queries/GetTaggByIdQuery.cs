
using MediatR;
using System.ComponentModel.DataAnnotations;
using TaggTimeline.ClientModel.Taggs;

namespace TaggTimeline.Service.Queries;

public class GetTaggByIdQuery : IRequest<TaggModel>
{
    [Required]
    public Guid Id { get; set; }
}
