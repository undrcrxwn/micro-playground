using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MicroPlayground.IdentityServer.Controllers;

[ApiController, Route("roles")]
public class AssignRoleController : ControllerBase
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AssignRoleController(
        RoleManager<IdentityRole> roleManager,
        UserManager<IdentityUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    [HttpPost("assign-admin")]
    public async Task AssignAdmin([FromQuery] string username)
    {
        var user =
            await _userManager.FindByNameAsync(username)
            ?? throw new InvalidOperationException($"No user '{username}' found");

        if (!await _roleManager.RoleExistsAsync("Admin"))
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
        
        await _userManager.AddToRoleAsync(user, "Admin");
    }
}