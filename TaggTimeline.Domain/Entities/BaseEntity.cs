
using System.ComponentModel.DataAnnotations;

namespace TaggTimeline.Domain.Entities;

public class BaseEntity
{
    [Key]
    [Required]
    public Guid Id { get; set; }
}
