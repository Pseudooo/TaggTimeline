
using System.ComponentModel.DataAnnotations;

namespace TaggTimeline.ClientModel.Taggs;

public class TaggModel
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public IEnumerable<InstanceModel> Instances { get; set; } = null!;

    public IEnumerable<CategoryPreviewModel> Categories { get; set; } = null!;
}
