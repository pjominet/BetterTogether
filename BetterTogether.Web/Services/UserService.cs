using BetterTogether.Web.Data;
using BetterTogether.Web.Infrastructure.Settings;
using BetterTogether.Web.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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
    private readonly RoleManager<IdentityRole> _roleManager;

    private readonly UserSeedSettings _userSeedSettings;

    public UserService(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<UserSeedSettings> userSeedOptions)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _roleManager = roleManager;

        _userSeedSettings = userSeedOptions.Value;
    }

    public Task<bool> HasAnyUsers()
    {
        return _dbContext.Users.AnyAsync();
    }

    public async Task SeedDefaultAdmin()
    {
        if (!await _roleManager.RoleExistsAsync(Roles.AdminRole))
            await _roleManager.CreateAsync(new IdentityRole(Roles.AdminRole));

        if (await _userManager.FindByEmailAsync(_userSeedSettings.UserName) is not null)
            return;

        var masterAdmin = new IdentityUser
        {
            Email = _userSeedSettings.UserName,
            UserName = _userSeedSettings.UserName,
            EmailConfirmed = true,
            PasswordHash = _userManager.PasswordHasher.HashPassword(null!, _userSeedSettings.Password)
        };

        var result = await _userManager.CreateAsync(masterAdmin);

        if (result.Succeeded)
            await _userManager.AddToRoleAsync(masterAdmin, Roles.AdminRole);
    }
}
