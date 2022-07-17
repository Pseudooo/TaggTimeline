
using System.ComponentModel.DataAnnotations;

namespace TaggTimeline.Domain.Entities;

public class DatedEntity : BaseEntity
{
    [Required]
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
}
