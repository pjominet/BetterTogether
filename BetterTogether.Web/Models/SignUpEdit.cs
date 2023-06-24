using System.ComponentModel.DataAnnotations;

namespace BetterTogether.Web.Models;

public class SignUpEdit
{
    [Required] public int EventId { get; set; }
    [Required] public required string Name { get; set; }
    [Required] public required string Email { get; set; }
}
