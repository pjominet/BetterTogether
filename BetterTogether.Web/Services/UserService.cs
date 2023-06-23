using BetterTogether.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BetterTogether.Web.Services;

public interface IUserService
{
    Task<bool> HasAnyUsers();
    Task SeedDefaultAdmin();
}

public class UserService : IUserService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<IdentityUser> _userManager;

    private const string AdminEmail = "pat00@better-togther.lu";

    public UserService(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public Task<bool> HasAnyUsers()
    {
        return _dbContext.Users.AnyAsync();
    }

    public async Task SeedDefaultAdmin()
    {
        if (await _userManager.FindByEmailAsync(AdminEmail) != null)
            return;

        var masterAdmin = new IdentityUser
        {
            Email = AdminEmail,
            UserName = AdminEmail,
            EmailConfirmed = true,
            PasswordHash = _userManager.PasswordHasher.HashPassword(null!, "gugigu1990")
        };

        await _userManager.CreateAsync(masterAdmin);
    }
}
