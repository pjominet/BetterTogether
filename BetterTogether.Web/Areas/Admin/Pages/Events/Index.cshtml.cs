using BetterTogether.Web.Data.Entities;
using BetterTogether.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BetterTogether.Web.Areas.Admin.Pages.Events;

public class IndexModel : PageModel
{
    private readonly IEventService _eventService;
    private readonly ISignUpService _signUpService;

    public IndexModel(IEventService eventService, ISignUpService signUpService)
    {
        _eventService = eventService;
        _signUpService = signUpService;
    }

    [TempData] public bool PurgeSuccess { get; set; }

    public IEnumerable<Event> Events { get; set; }

    public async Task OnGetAsync()
    {
        Events = await _eventService.GetEvents();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int eventId)
    {
        await _eventService.DeleteEvent(eventId);

        return RedirectToPage("./Index");
    }

    public async Task<IActionResult> OnPostPurgeAsync()
    {
        if (await _signUpService.PurgeSignUpsWithoutEvents())
            PurgeSuccess = true;

        PurgeSuccess = false;

        return RedirectToPage("./Index");
    }
}
