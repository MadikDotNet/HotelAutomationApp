using System;
using System.Threading.Tasks;
using HotelAutomationApp.Application.Auth.Models;
using HotelAutomationApp.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace HotelAutomationApp.WebApi.Seeds;

public class IdentitySeed
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentitySeed(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task ApplySeed()
    {
        await InitializeRoles();
    }

    private async Task InitializeRoles()
    {
        foreach (var role in Role.Names)
        {
            if (await _roleManager.FindByNameAsync(role) is null)
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        var (rootUsername, rootPassword, rootEmail) = ("root", "r00t2O22", "moldageldinmadik@gmail.com");
        if (await _userManager.FindByNameAsync(rootUsername) is null)
        {
            var root = new ApplicationUser
            {
                Email = rootEmail,
                UserName = rootUsername,
                CreatedDate = DateTime.UtcNow
            };

            await _userManager.CreateAsync(root, rootPassword);

            await _userManager.AddToRoleAsync(root, Role.Root.ToString());
        }

        var (userUsername, userPassword, userEmail) = ("Happy_Saram", "bota3112", "Zhanbota_bota@mail.ru");
        if (await _userManager.FindByNameAsync(userUsername) is null)
        {
            var admin = new ApplicationUser
            {
                Email = userEmail,
                UserName = userUsername,
                CreatedDate = DateTime.UtcNow
            };

            await _userManager.CreateAsync(admin, userPassword);
            await _userManager.AddToRoleAsync(admin, Role.Admin.ToString());
        }
    }
}