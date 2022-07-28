using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaggTimeline.Domain.Entities.Users;

namespace TaggTimeline.Domain.Entities.Taggs;

public class Tagg : KeyedEntity, IUserOwnedEntity
{
    public IEnumerable<Instance> Instances { get; set; } = null!;
    public IEnumerable<Category> Categories { get; set; } = null!;
    
    [Required]
    public UserMapping UserMapping { get; set; } = null!;
}
