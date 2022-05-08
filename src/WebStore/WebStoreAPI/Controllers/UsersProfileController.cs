using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using WebStore.Logic.Interfaces;
using WebStore.Logic.Models.UserProfile;

namespace WebStore.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UsersProfileController : ControllerBase
{
    private readonly IUsersProfileService _usersProfileService;

    public UsersProfileController(IUsersProfileService usersProfileService)
    {
        _usersProfileService = usersProfileService;
    }

    private Guid UserId => Guid.ParseExact(User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value, "D");

    [HttpGet]
    public async Task<IActionResult> Read()
    {
        var result = await _usersProfileService.GetDetailsAsync(UserId);
        return Ok(result);
    }

    [HttpPatch]
    public async Task<IActionResult> Patch(UpdateProfileModel updateProfileModel)
    {
        var result = await _usersProfileService.UpdateAsync(UserId, updateProfileModel);
        return Ok(result);
    }
}