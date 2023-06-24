using BetterTogether.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BetterTogether.Web.Pages;

[AllowAnonymous]
public class IndexModel : PageModel
{
    private readonly IEventService _eventService;

    public IndexModel(IEventService eventService)
    {
        _eventService = eventService;
    }

    public bool HasEvents { get; set; }

    public async Task OnGetAsync()
    {
        HasEvents = (await _eventService.GetActiveEvents()).Any();
    }
}
