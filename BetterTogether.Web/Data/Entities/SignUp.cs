namespace BetterTogether.Web.Data.Entities;

public class SignUp
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public DateTime CreationDate { get; set; }

    public ICollection<EventSignUp> Events { get; set; } = new HashSet<EventSignUp>();
}
