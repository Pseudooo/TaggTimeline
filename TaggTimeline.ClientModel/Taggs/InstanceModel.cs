
using System.ComponentModel.DataAnnotations;

namespace TaggTimeline.ClientModel.Taggs;

public class InstanceModel
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    public DateTime CreatedDate { get; set; }
    
    [Required]
    public DateTime OccuranceDate { get; set; }
}
