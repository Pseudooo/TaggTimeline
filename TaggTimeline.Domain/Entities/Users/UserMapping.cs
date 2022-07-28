
using System.ComponentModel.DataAnnotations;
using TaggTimeline.Domain.Entities.Taggs;

namespace TaggTimeline.Domain.Entities.Users;

public class UserMapping : BaseEntity
{
    [Required]
    public string UserId { get; set; } = null!;

    public IEnumerable<Tagg> Taggs { get; set; } = null!;
    public IEnumerable<Category> Categories { get; set; } = null!;
}
