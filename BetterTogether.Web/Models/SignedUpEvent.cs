using BetterTogether.Web.Data.Entities;

namespace BetterTogether.Web.Models;

public class SignedUpEvent
{
    public string SignedUpEmail { get; set; }
    public Event Event { get; set; } = null!;
    public int SignUpCount { get; set; }
}
