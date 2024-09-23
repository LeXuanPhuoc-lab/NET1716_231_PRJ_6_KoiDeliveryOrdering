using KoiDeliveryOrdering.API.Payloads;
using KoiDeliveryOrdering.Business.Base;
using Microsoft.AspNetCore.Mvc;

namespace KoiDeliveryOrdering.API.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    [HttpGet(ApiRoute.User.GetById, Name = nameof(GetUserByIdAsync))]
    public async Task<IBusinessResult> GetUserByIdAsync(Guid userId)
    {
        await Task.CompletedTask;
        return new BusinessResult
        {
            Status = 200,
            Message = "User not found",
            Data = null
        };
        // return await _productService.GetAll(); return theo cai t cmt thì sẽ giong cua thay, cai tren t de nhu vay de no ko loi
    }
}