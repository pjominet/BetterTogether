using BetterTogether.Web.Data;
using BetterTogether.Web.Data.Entities;
using BetterTogether.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace BetterTogether.Web.Services;

public interface ISignUpService
{
    Task<SignedUpEvent?> AddEventSignUp(SignUpEdit signUpEdit);
    Task<bool> RemoveEventSignUp(int signUpId, int eventId);
    Task<SignUp?> GetSignUp(string email);
    Task<IEnumerable<SignedUpEvent>> GetSignedUpEvents(string email);
    Task<bool> PurgeSignUpsWithoutEvents();
}

public class SignUpService : ISignUpService
{

    private readonly ApplicationDbContext _dbContext;

    public SignUpService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<SignedUpEvent?> AddEventSignUp(SignUpEdit signUpEdit)
    {
        var signUpEntity = await _dbContext.SignUps.FirstOrDefaultAsync(s => s.Email == signUpEdit.Email);

        if (signUpEntity is null)
        {
            signUpEntity = new SignUp
            {
                Name = signUpEdit.Name,
                Email = signUpEdit.Email
            };

            await _dbContext.SignUps.AddAsync(signUpEntity);
            if (await _dbContext.SaveChangesAsync() <= 0)
                return null;
        }

        var eventSignUpEntity = new EventSignUp
        {
            EventId = signUpEdit.EventId,
            SignUpId = signUpEntity.Id
        };

        await _dbContext.AddAsync(eventSignUpEntity);
        return await _dbContext.SaveChangesAsync() > 0
            ? new SignedUpEvent
            {
                SignedUpEmail = signUpEntity.Email,
                Event = await _dbContext.Events.Where(e => e.Id == signUpEdit.EventId).FirstAsync()
            }
            : null;
    }

    public async Task<bool> RemoveEventSignUp(int signUpId, int eventId)
    {
        var existingSignUp = await _dbContext.EventSignUps.FirstOrDefaultAsync(es => es.SignUpId == signUpId && es.EventId == eventId);
        if (existingSignUp is null)
            return false;

        _dbContext.Remove(existingSignUp);

        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<SignUp?> GetSignUp(string email)
    {
        return await _dbContext.SignUps
            .FirstOrDefaultAsync(s => s.Email == email);
    }

    public async Task<IEnumerable<SignedUpEvent>> GetSignedUpEvents(string email)
    {
        var query = from s in _dbContext.SignUps
            join es in _dbContext.EventSignUps on s.Id equals es.SignUpId
            join e in _dbContext.Events on es.EventId equals e.Id
            where s.Email == email
            select es;

        var eventSignUpEntities = await query
            .Include(es => es.Event)
            .ToListAsync();

        return eventSignUpEntities.Select(eventSignUp => new SignedUpEvent
        {
            SignedUpEmail = email,
            Event = eventSignUp.Event!,
            SignUpCount = eventSignUpEntities.Count(es => es.EventId == eventSignUp.EventId)
        });
    }

    public async Task<bool> PurgeSignUpsWithoutEvents()
    {
        var signUps = await _dbContext.SignUps
            .Where(s => _dbContext.EventSignUps.Select(es => es.SignUpId).Contains(s.Id))
            .ToListAsync();

        _dbContext.RemoveRange(signUps);
        return await _dbContext.SaveChangesAsync() > 0;
    }
}
