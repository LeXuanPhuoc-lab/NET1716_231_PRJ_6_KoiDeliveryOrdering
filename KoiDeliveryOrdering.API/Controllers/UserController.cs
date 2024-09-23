using KoiDeliveryOrdering.API.Payloads;
using KoiDeliveryOrdering.Business;
using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KoiDeliveryOrdering.API.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userServices;

    public UserController(UserService userServices)
    {
        _userServices = userServices;
    }
    [HttpGet(ApiRoute.User.GetById, Name = nameof(GetUserByIdAsync))]
    public async Task<IBusinessResult> GetUserByIdAsync(Guid userId)
    {
        
        return await _userServices.FindAsync(userId);
    }
}