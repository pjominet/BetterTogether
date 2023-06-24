using BetterTogether.Web.Data.Entities;
using BetterTogether.Web.Models;
using BetterTogether.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BetterTogether.Web.Areas.Admin.Pages.Events;

public class EditModel : PageModel
{
    private readonly IEventService _eventService;

    public EditModel(IEventService eventService)
    {
        _eventService = eventService;
    }

    public Event Event { get; set; }

    [BindProperty]
    public EventEdit EventEdit { get; set; }

    [TempData]
    public string ErrorMessage { get; set; }

    public async Task OnGetAsync(int eventId)
    {
        Event = await _eventService.GetEvent(eventId) ?? throw new ApplicationException($"Cannot find event to edit with ID {eventId}");
    }

    public async Task<IActionResult> OnPostAsync(int eventId)
    {
        if (await _eventService.UpdateEvent(eventId, EventEdit))
            return RedirectToPage("./Index");

        ErrorMessage = "Edit failed";
        return RedirectToPage("./Edit", new { eventId });
    }
}
