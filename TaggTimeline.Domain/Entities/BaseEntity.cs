
using System.ComponentModel.DataAnnotations;

namespace TaggTimeline.Domain.Entities;

public class BaseEntity
{

    [Key]
    public Guid Id { get; set; }

}
