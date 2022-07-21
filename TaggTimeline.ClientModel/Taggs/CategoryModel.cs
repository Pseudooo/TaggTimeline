
using System.ComponentModel.DataAnnotations;

namespace TaggTimeline.ClientModel.Taggs;

public class CategoryModel
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }
    
    public DateTime? DeletedDate { get; set; }

    public IEnumerable<TaggPreviewModel> Taggs { get; set; } = null!;
}
