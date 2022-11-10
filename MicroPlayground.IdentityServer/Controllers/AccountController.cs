using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MicroPlayground.IdentityServer.Controllers;

[ApiController, Route("account")]
public class AccountController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<IdentityUser> _userManager;

    public AccountController(
        ApplicationDbContext dbContext,
        UserManager<IdentityUser> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromQuery] string username, [FromQuery] string password)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user is not null)
            return BadRequest("User already exists");

        var identity = new IdentityUser(username);
        var result = await _userManager.CreateAsync(identity, password);
        
        return result.Succeeded
            ? Ok($"Created user '{username}:{password}'")
            : BadRequest(result.Errors);
    }

    [HttpGet("get-name"), Authorize]
    public IActionResult GetMyName()
    {
        return Ok(User.Identity?.Name ?? "<no-identity>");
    }
}