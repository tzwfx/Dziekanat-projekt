using CoreApp.Entities;
using Infrastructure.EntityFramework.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Security;

public interface IDataSeeder
{
    int Order { get; }
    Task SeedAsync();
}

public class IdentityDbSeeder : IDataSeeder
{
    public int Order => 1;

    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly ILogger<IdentityDbSeeder> _logger;

    public IdentityDbSeeder(
        UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager,
        ILogger<IdentityDbSeeder> logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _logger      = logger;
    }

    public async Task SeedAsync()
    {
        await SeedRolesAsync();
        await SeedUsersAsync();
    }

    private async Task SeedRolesAsync()
    {
        var roles = new[]
        {
            new AppRole(UserRole.Administrator.ToString(),    "Pełny dostęp do systemu."),
            new AppRole(UserRole.DeanOfficeWorker.ToString(), "Pracownik dziekanatu."),
            new AppRole(UserRole.Lecturer.ToString(),         "Wykładowca."),
            new AppRole(UserRole.Student.ToString(),          "Student.")
        };

        foreach (var role in roles)
        {
            if (await _roleManager.RoleExistsAsync(role.Name!))
                continue;
            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
                _logger.LogError("Błąd tworzenia roli {Role}", role.Name);
        }
    }

    private async Task SeedUsersAsync()
    {
        var users = new[]
        {
            new SeedUser("F5BADE14-6CC8-42A2-9A44-9842DA2D9280", "admin@uczelnia.pl",
                "Admin", "System", "IT", "Admin@123!", UserRole.Administrator),
            new SeedUser("93A7FFDD-057F-4021-9C68-FE06951FFA65", "dziekanat@uczelnia.pl",
                "Anna", "Kowalska", "Dziekanat", "Dziekanat@123!", UserRole.DeanOfficeWorker)
        };

        foreach (var seedUser in users)
            await CreateUserAsync(seedUser);
    }

    private async Task CreateUserAsync(SeedUser seedUser)
    {
        if (await _userManager.FindByEmailAsync(seedUser.Email) is not null)
        {
            _logger.LogInformation("Użytkownik {Email} już istnieje — pomijam.", seedUser.Email);
            return;
        }

        var user = new AppUser
        {
            Id             = seedUser.Id,
            UserName       = seedUser.Email,
            Email          = seedUser.Email,
            NormalizedEmail = seedUser.Email.ToUpper(),
            EmailConfirmed = true,
            FirstName      = seedUser.FirstName,
            LastName       = seedUser.LastName,
            FullName       = $"{seedUser.FirstName} {seedUser.LastName}",
            Department     = seedUser.Department,
            Status         = SystemUserStatus.Active,
            LockoutEnabled = true
        };

        var createResult = await _userManager.CreateAsync(user, seedUser.Password);
        if (!createResult.Succeeded)
        {
            _logger.LogError("Błąd tworzenia użytkownika {Email}", seedUser.Email);
            return;
        }

        await _userManager.AddToRoleAsync(user, seedUser.Role.ToString());
        _logger.LogInformation("Utworzono użytkownika {Email}", seedUser.Email);
    }
}

internal record SeedUser(
    string Id, string Email, string FirstName, string LastName,
    string Department, string Password, UserRole Role);