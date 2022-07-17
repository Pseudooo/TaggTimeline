
using System.ComponentModel.DataAnnotations;

namespace TaggTimeline.Domain.Entities.Taggs;

public class Tagg : DatedEntity
{
    [Required]
    [MaxLength(255)]
    public string Key { get; set; }
}
