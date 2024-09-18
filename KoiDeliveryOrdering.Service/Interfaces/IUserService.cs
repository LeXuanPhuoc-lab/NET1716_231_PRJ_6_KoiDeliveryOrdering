using System.Linq.Expressions;
using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Data.Dtos;
using KoiDeliveryOrdering.Data.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace KoiDeliveryOrdering.Business.Interfaces;

public interface IUserService
{
    // Basic
    Task<IBusinessResult> InsertAsync(UserDto test);
    Task<IBusinessResult> RemoveAsync(Guid userId);
    Task<IBusinessResult> UpdateAsync(UserDto test);
    Task<IBusinessResult> FindAsync(Guid userId);
    Task<IBusinessResult> FindAllAsync();
    Task<IBusinessResult> FindOneWithConditionAsync(
        Expression<Func<User, User>>? filter,
        Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = null,
        string? includeProperties = "");
    Task<IBusinessResult> FindAllWithConditionAsync(
        Expression<Func<User, User>>? filter = null,
        Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = null,
        string? includeProperties = "");
    Task<IBusinessResult> FindAllWithConditionAndThenIncludeAsync(
        Expression<Func<User, bool>>? filter = null,
        Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = null,
        List<Func<IQueryable<User>, IIncludableQueryable<User, object>>>? includes = null);
}