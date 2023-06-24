namespace BetterTogether.Web.Data.Entities;

public class EventSignUp
{
    public int EventId { get; set; }
    public int SignUpId { get; set; }

    public Event? Event { get; set; }
    public SignUp? SignUp { get; set; }
}
