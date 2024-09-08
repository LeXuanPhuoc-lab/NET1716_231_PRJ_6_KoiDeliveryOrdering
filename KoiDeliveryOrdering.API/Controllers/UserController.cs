using KoiDeliveryOrdering.API.Payloads;
using Microsoft.AspNetCore.Mvc;

namespace KoiDeliveryOrdering.API.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    [HttpGet(ApiRoute.User.GetById, Name = nameof(GetUserByIdAsync))]
    public async Task<IActionResult> GetUserByIdAsync(Guid userId)
    {
        await Task.CompletedTask;
        return Ok();
    }
}