
using System.ComponentModel.DataAnnotations;

namespace TaggTimeline.Domain.Entities.Taggs;

public class Tagg : MutableDatedEntity
{
    [Required]
    [MaxLength(255)]
    public string Key { get; set; } = null!;
    public IEnumerable<Instance> Instances { get; set; } = null!;
}
