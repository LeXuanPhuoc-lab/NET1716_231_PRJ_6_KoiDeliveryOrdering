using KoiDeliveryOrdering.API.Payloads;
using KoiDeliveryOrdering.Business;
using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Business.Interfaces;
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
    public async Task<IServiceResult> GetUserByIdAsync(Guid userId)
    {
        
        return await _userService.FindAsync(userId);
    }

    [HttpGet(ApiRoute.User.GetAll, Name = nameof(GetAllAsync))]
    public async Task<IServiceResult> GetAllAsync()
    {
        return await _userService.FindAllAsync(); 
    }
}