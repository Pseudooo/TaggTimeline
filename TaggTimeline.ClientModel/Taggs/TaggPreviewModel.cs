using System.ComponentModel.DataAnnotations;

namespace TaggTimeline.ClientModel.Taggs;

public class TaggPreviewModel
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    public string Key { get; set; }  = null!;
}

