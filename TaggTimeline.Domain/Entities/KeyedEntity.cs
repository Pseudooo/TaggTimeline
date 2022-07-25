
using System.ComponentModel.DataAnnotations;

namespace TaggTimeline.Domain.Entities;

public class KeyedEntity : MutableDatedEntity
{
    [Required]
    [MaxLength(255)]
    public string Key { get; set; } = null!;
}
