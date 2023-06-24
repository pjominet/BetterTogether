using BetterTogether.Web.Data.Entities;
using BetterTogether.Web.Models;
using BetterTogether.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BetterTogether.Web.Areas.Admin.Pages.Events;

public class CreateModel : PageModel
{
    private readonly IEventService _eventService;

    public CreateModel(IEventService eventService)
    {
        _eventService = eventService;
    }

    [BindProperty] public EventEdit EventEdit { get; set; }

    [TempData] public string ErrorMessage { get; set; }

    public void OnGet()
    {
        EventEdit = new EventEdit
        {
            IsActive = true,
            StartDate = DateTime.Now.AddHours(1)
        };
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var @event = new Event
        {
            Name = EventEdit.Name,
            Description = EventEdit.Description,
            StartDate = EventEdit.StartDate,
            IsActive = EventEdit.IsActive
        };

        await _eventService.CreateEvent(@event);

        return RedirectToPage("./Index");
    }
}
