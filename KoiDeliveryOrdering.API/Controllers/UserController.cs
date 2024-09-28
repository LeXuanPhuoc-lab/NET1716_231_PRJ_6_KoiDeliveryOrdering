using KoiDeliveryOrdering.API.Payloads;
using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KoiDeliveryOrdering.API.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet(ApiRoute.User.GetById, Name = nameof(GetUserByIdAsync))]
    public async Task<IActionResult> GetUserByIdAsync(Guid userId)
    {
        await Task.CompletedTask;
        return Ok();
    }

    [HttpGet(ApiRoute.User.GetAll, Name = nameof(GetAllAsync))]
    public async Task<IServiceResult> GetAllAsync()
    {
        return await _userService.FindAllAsync(); 
    }
}