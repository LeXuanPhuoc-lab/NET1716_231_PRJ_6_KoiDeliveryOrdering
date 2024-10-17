using System.Linq.Expressions;
using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Data.Dtos;
using KoiDeliveryOrdering.Data.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace KoiDeliveryOrdering.Business.Interfaces;

public interface IUserService
{
    // Basic
    Task<IServiceResult> InsertAsync(UserDto test);
    Task<IServiceResult> RemoveAsync(Guid userId);
    Task<IServiceResult> UpdateAsync(UserDto test);
    Task<IServiceResult> FindByUsernameAsync(string username);
    Task<IServiceResult> FindAsync(Guid userId);
    Task<IServiceResult> FindAllAsync();
    Task<IServiceResult> FindOneWithConditionAsync(
        Expression<Func<UserDTO, UserDTO>>? filter,
        Func<IQueryable<UserDTO>, IOrderedQueryable<UserDTO>>? orderBy = null,
        string? includeProperties = "");
    Task<IServiceResult> FindAllWithConditionAsync(
        Expression<Func<UserDTO, UserDTO>>? filter = null,
        Func<IQueryable<UserDTO>, IOrderedQueryable<UserDTO>>? orderBy = null,
        string? includeProperties = "");
    Task<IServiceResult> FindAllWithConditionAndThenIncludeAsync(
        Expression<Func<UserDTO, bool>>? filter = null,
        Func<IQueryable<UserDTO>, IOrderedQueryable<UserDTO>>? orderBy = null,
        List<Func<IQueryable<UserDTO>, IIncludableQueryable<UserDTO, object>>>? includes = null);

    // Additional

    Task<IServiceResult> FindAllVoucherByUsernameAsync(string username);

    // This function only use when application authentication not implemented yet.
    Task<IServiceResult> FindAllSenderInformationAsync();
}