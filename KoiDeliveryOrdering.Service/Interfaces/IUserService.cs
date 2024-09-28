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
    Task<IServiceResult> FindAsync(Guid userId);
    Task<IServiceResult> FindAllAsync();
    Task<IServiceResult> FindOneWithConditionAsync(
        Expression<Func<User, User>>? filter,
        Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = null,
        string? includeProperties = "");
    Task<IServiceResult> FindAllWithConditionAsync(
        Expression<Func<User, User>>? filter = null,
        Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = null,
        string? includeProperties = "");
    Task<IServiceResult> FindAllWithConditionAndThenIncludeAsync(
        Expression<Func<User, bool>>? filter = null,
        Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = null,
        List<Func<IQueryable<User>, IIncludableQueryable<User, object>>>? includes = null);
}