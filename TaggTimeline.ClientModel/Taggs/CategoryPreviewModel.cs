
using System.ComponentModel.DataAnnotations;

namespace TaggTimeline.ClientModel.Taggs;

public class CategoryPreviewModel
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string Key { get; set; } = null!;
}
