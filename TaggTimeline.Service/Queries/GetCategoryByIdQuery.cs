
using System.ComponentModel.DataAnnotations;
using MediatR;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;

namespace TaggTime.Service.Queries;

public class GetCategoryByIdQuery : IRequest<CategoryModel>
{
    [Required]
    public Guid Id { get; set; }
}
