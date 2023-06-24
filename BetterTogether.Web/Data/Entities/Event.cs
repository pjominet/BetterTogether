namespace BetterTogether.Web.Data.Entities;

public class Event
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime CreationDate { get; set; }

    public ICollection<EventSignUp> SignUps { get; set; } = new HashSet<EventSignUp>();
}
