using KoiDeliveryOrdering.API.Payloads;
using KoiDeliveryOrdering.Business;
using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Business.Interfaces;
using KoiDeliveryOrdering.Common;
using KoiDeliveryOrdering.Data.Dtos;
using KoiDeliveryOrdering.Data.Entities;
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

    [HttpGet(ApiRoute.User.GetAllSenderInformationAsync, Name = nameof(GetAllSenderInformationAsync))]
    public async Task<IServiceResult> GetAllSenderInformationAsync()
    {
        return await _userService.FindAllSenderInformationAsync();
    }

    [HttpGet(ApiRoute.User.GetByUsername, Name = nameof(GetByUsernameAsync))]
    public async Task<IServiceResult> GetByUsernameAsync([FromRoute] string username)
    {
        return await _userService.FindByUsernameAsync(username);
    }

    [HttpDelete(ApiRoute.User.Remove)]
    public async Task<IServiceResult> RemoveUserAsync([FromRoute] Guid id)
    {
        return await _userService.RemoveAsync(id);
    }
    [HttpPost(ApiRoute.User.Insert)]
    public async Task<IServiceResult> InsertUserAsync([FromBody] UserDto userModel)
    {
            return await _userService.InsertAsync(userModel);  
    }

    [HttpPut(ApiRoute.User.Update)]
    public async Task<IServiceResult> UpdateUserAsync([FromBody] UserDto req)
    {
        if (req == null) throw new BadHttpRequestException("Request is wrong");
        return await _userService.UpdateAsync(req);
    }
}