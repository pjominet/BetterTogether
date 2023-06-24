using BetterTogether.Web.Data;
using BetterTogether.Web.Data.Entities;
using BetterTogether.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace BetterTogether.Web.Services;

public interface IEventService
{
    Task<int> CreateEvent(Event @event);
    Task<bool> UpdateEvent(int eventId, EventEdit eventEdit);
    Task<bool> DeleteEvent(int eventId);
    Task<Event?> GetEvent(int eventId);
    Task<IEnumerable<Event>> GetEvents();
    Task<IEnumerable<Event>> GetActiveEvents(IEnumerable<int>? excludedEvents = null);
}

public class EventService : IEventService
{
    private readonly ApplicationDbContext _dbContext;

    public EventService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> CreateEvent(Event @event)
    {
        await _dbContext.AddAsync(@event);
        await _dbContext.SaveChangesAsync();
        return @event.Id;
    }

    public async Task<bool> UpdateEvent(int eventId, EventEdit eventEdit)
    {
        var existingEvent = await _dbContext.Events.FirstOrDefaultAsync(e => e.Id == eventId);

        if (existingEvent is null)
            return false;

        existingEvent.Name = eventEdit.Name;
        existingEvent.StartDate = eventEdit.StartDate;
        existingEvent.IsActive = eventEdit.IsActive;

        if (eventEdit.IsActive)
            return await _dbContext.SaveChangesAsync() > 0;

        var obsoleteSignUps = await _dbContext.EventSignUps
            .Where(es => es.EventId == existingEvent.Id)
            .ToListAsync();
        _dbContext.RemoveRange(obsoleteSignUps);

        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteEvent(int eventId)
    {
        var existingEvent = await _dbContext.Events.FirstOrDefaultAsync(e => e.Id == eventId);
        if (existingEvent is null)
            return false;

        _dbContext.Remove(existingEvent);

        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<Event?> GetEvent(int eventId)
    {
        return await _dbContext.Events.FirstOrDefaultAsync(e => e.Id == eventId);
    }

    public async Task<IEnumerable<Event>> GetEvents()
    {
        return await _dbContext.Events.ToListAsync();
    }

    public async Task<IEnumerable<Event>> GetActiveEvents(IEnumerable<int>? excludedEvents = null)
    {
        var query = _dbContext.Events
            .Where(e => e.IsActive && e.StartDate >= DateTime.Now);

        if (excludedEvents is not null)
            query = query.Where(e => !excludedEvents.Contains(e.Id));

        return await query.ToListAsync();
    }
}
