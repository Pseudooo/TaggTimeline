using System.ComponentModel.DataAnnotations;

namespace TaggTimeline.Domain.Entities.Taggs;

public class Tagg : KeyedEntity, IUserOwnedEntity
{
    public IEnumerable<Instance> Instances { get; set; } = null!;
    public IEnumerable<Category> Categories { get; set; } = null!;

    [Required]
    public string Colour { get; set; } = null!;
    
    public string UserId { get; set; } = null!;
}
