
using System.ComponentModel.DataAnnotations;

namespace TaggTimeline.ClientModel.Auth;

public class AuthenticationResultModel
{
    [Required]
    public string Token { get; set; } = null!;
}
