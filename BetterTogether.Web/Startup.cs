using System.Globalization;
using BetterTogether.Web.Data;
using BetterTogether.Web.Infrastructure.Settings;
using BetterTogether.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;

namespace BetterTogether.Web;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection")
                               ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(connectionString));

        services.AddDatabaseDeveloperPageExceptionFilter();

        services
            .AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddRazorPages()
            .AddRazorRuntimeCompilation();

        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });

        services
            .AddScoped<IUserService, UserService>()
            .AddScoped<IEventService, EventService>()
            .AddScoped<ISignUpService, SignUpService>();

        services.Configure<UserSeedSettings>(_configuration.GetSection(nameof(UserSeedSettings)));
    }

    public static async Task Configure(WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
            app.UseMigrationsEndPoint();
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        var appCulture = CultureInfo.CreateSpecificCulture("fr-FR");

        var supportedCultures = new[] { appCulture };

        app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture(appCulture),
            SupportedCultures = supportedCultures,
            SupportedUICultures = supportedCultures
        });

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        using var scope = app.Services.CreateScope();
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
        if (!await userService.HasAnyUsers())
            await userService.SeedDefaultAdmin();
    }
}

public static class WebApplicationExtensions
{
    public static async Task Configure(this WebApplication app)
    {
        await Startup.Configure(app);
    }
}
