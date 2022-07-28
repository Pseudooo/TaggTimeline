
using System.ComponentModel.DataAnnotations;

namespace TaggTimeline.Domain.Entities.Users;

public class UserMapping : BaseEntity
{
    [Required]
    public string UserId { get; set; } = null!;
}
