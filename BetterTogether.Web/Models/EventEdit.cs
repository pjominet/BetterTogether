using System.ComponentModel.DataAnnotations;

namespace BetterTogether.Web.Models;

public class EventEdit
{
    [Required] public string Name { get; set; }
    [Required] public string Description { get; set; }
    [Required] public DateTime StartDate { get; set; }
    public bool IsActive { get; set; } = true;
}
