
using System.ComponentModel.DataAnnotations;

namespace TaggTimeline.Domain.Entities.Taggs;

public class Instance : DatedEntity
{
    [Required]
    public DateTime OccuranceDate { get; set; }
}
