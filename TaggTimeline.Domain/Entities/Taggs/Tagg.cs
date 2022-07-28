using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaggTimeline.Domain.Entities.Users;

namespace TaggTimeline.Domain.Entities.Taggs;

public class Tagg : KeyedEntity
{
    public IEnumerable<Instance> Instances { get; set; } = null!;
    public IEnumerable<Category> Categories { get; set; } = null!;
    
    [ForeignKey("UserMapping")]
    public Guid UserMappingId { get; set; }
    [Required]
    public UserMapping UserMapping { get; set; } = null!;
}
