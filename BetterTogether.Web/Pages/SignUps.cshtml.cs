using BetterTogether.Web.Data.Entities;
using BetterTogether.Web.Infrastructure;
using BetterTogether.Web.Models;
using BetterTogether.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BetterTogether.Web.Pages;

[AllowAnonymous]
public class SignUpsModel : PageModel
{
    private readonly ISignUpService _signUpService;
    private readonly IEventService _eventService;

    public SignUpsModel(ISignUpService signUpService, IEventService eventService)
    {
        _signUpService = signUpService;
        _eventService = eventService;
    }

    public SignUp? SignUp { get; set; }
    public IEnumerable<SignedUpEvent> SignedUpEvents { get; set; }
    public IEnumerable<Event> AvailableEvents { get; set; }

    [TempData] public bool? SignUpSuccess { get; set; }

    [TempData] public bool? DeleteSuccess { get; set; }

    [BindProperty] public SignUpEdit SignUpEdit { get; set; }

    public async Task OnGetAsync()
    {
        var signUpEmail = Request.Cookies["signUp"];
        if (signUpEmail.HasValue())
        {
            SignUp = await _signUpService.GetSignUp(signUpEmail!);
            SignedUpEvents = await _signUpService.GetSignedUpEvents(signUpEmail!);
        }

        AvailableEvents = await _eventService.GetActiveEvents(SignedUpEvents.Select(se => se.Event.Id));
    }

    public async Task<IActionResult> OnPostAddAsync()
    {
        if (!ModelState.IsValid)
            return RedirectToPage("./SignUps");

        var signUpEvent = await _signUpService.AddEventSignUp(SignUpEdit);

        if (signUpEvent is not null)
        {
            SignUpSuccess = true;
            Response.Cookies.Append("currentCardIndex", "2", new CookieOptions { MaxAge = TimeSpan.FromDays(30) });
            var expirationTime = signUpEvent.Event.StartDate - DateTime.Now;
            Response.Cookies.Append("signUp", signUpEvent.SignedUpEmail, new CookieOptions { MaxAge = expirationTime });
        }
        else SignUpSuccess = false;

        return RedirectToPage("./SignUps");
    }

    public async Task<IActionResult> OnPostDeleteAsync(int signUpId, int eventId)
    {
        if (await _signUpService.RemoveEventSignUp(signUpId, eventId))
            DeleteSuccess = true;

        DeleteSuccess = false;

        return RedirectToPage("./SignUps");
    }

    public async Task<IActionResult> OnGetEventDetailsAsync(int eventId)
    {
        return new JsonResult(await _eventService.GetEvent(eventId));
    }
}
