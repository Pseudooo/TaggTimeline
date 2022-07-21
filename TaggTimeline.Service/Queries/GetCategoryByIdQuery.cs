
using System.ComponentModel.DataAnnotations;
using MediatR;
using TaggTimeline.Domain.Entities.Taggs;

namespace TaggTime.Service.Queries;

public class GetCategoryByIdQuery : IRequest<Category>
{
    [Required]
    public Guid Id { get; set; }
}
