using System.Linq.Expressions;
using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Business.Interfaces;
using KoiDeliveryOrdering.Common;
using KoiDeliveryOrdering.Data;
using KoiDeliveryOrdering.Data.Dtos;
using KoiDeliveryOrdering.Data.Entities;
using MapsterMapper;
using Microsoft.EntityFrameworkCore.Query;

namespace KoiDeliveryOrdering.Business;

public class UserService(UnitOfWork unitOfWork, IMapper mapper) : IUserService
{
    public async Task<IBusinessResult> InsertAsync(UserDto test)
    {
        try
        {
            // Prepare insert 
            await unitOfWork.UserRepository.PrepareInsertAsync(mapper.Map<User>(test));
            
            // Perform insert query to db
            var isInserted = await unitOfWork.UserRepository.SaveChangeWithTransactionAsync() > 0;

            return isInserted
                // Insert successfully
                ? new BusinessResult(Const.SUCCESS_INSERT_CODE, Const.SUCCESS_INSERT_MSG)
                // Insert error
                : new BusinessResult(Const.FAIL_INSERT_CODE, Const.FAIL_INSERT_MSG);
        }
        catch (Exception ex)
        {
            // Invoke error
            return new BusinessResult(Const.ERROR_EXCEPTION_CODE, ex.Message);
        }
    }

    public async Task<IBusinessResult> RemoveAsync(Guid userId)
    {
        try
        {
            // Get by id 
            var userEntity = await unitOfWork.UserRepository.FindOneWithConditionAsync(u => 
                u.UserId == userId);
            
            if(userEntity == null) return new BusinessResult(Const.FAIL_REMOVE_CODE, Const.FAIL_REMOVE_MSG);
        
            // Prepare remove 
            await unitOfWork.UserRepository.PrepareRemoveAsync(userEntity);
        
            // Save to db
            var isRemoved = await unitOfWork.UserRepository.SaveChangeWithTransactionAsync() > 0;
        
            return isRemoved
                // Delete successfully
                ? new BusinessResult(Const.SUCCESS_REMOVE_CODE, Const.SUCCESS_REMOVE_MSG)
                // Delete fail
                : new BusinessResult(Const.FAIL_REMOVE_CODE, Const.FAIL_REMOVE_MSG);
        }
        catch (Exception ex)
        {
            // Invoke error
            return new BusinessResult(Const.ERROR_EXCEPTION_CODE, ex.Message);
        }
    }
    
    public async Task<IBusinessResult> UpdateAsync(UserDto user)
    {
        try
        {
            // Get by id 
            var userEntity = await unitOfWork.UserRepository.FindOneWithConditionAsync(u => 
                u.UserId == user.UserId);
        
            if(userEntity == null) return new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
        
            // Update entity properties 
            userEntity.Address = user.Address;
            
            await unitOfWork.UserRepository.UpdateAsync(userEntity, saveChanges: false);
        
            // Save to db
            var isUpdated = await unitOfWork.UserRepository.SaveChangeWithTransactionAsync() > 0;
        
            return isUpdated
                // Is delete success
                ? new BusinessResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG)
                // Invoke error
                : new BusinessResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
        }catch (Exception ex)
        {
            // Invoke error
            return new BusinessResult(Const.ERROR_EXCEPTION_CODE, ex.Message);
        }
    }

    public Task<IBusinessResult> FindAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<IBusinessResult> FindAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IBusinessResult> FindOneWithConditionAsync(Expression<Func<User, User>>? filter, Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = null, string? includeProperties = "")
    {
        throw new NotImplementedException();
    }

    public Task<IBusinessResult> FindAllWithConditionAsync(Expression<Func<User, User>>? filter = null, Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = null, string? includeProperties = "")
    {
        throw new NotImplementedException();
    }

    public Task<IBusinessResult> FindAllWithConditionAndThenIncludeAsync(Expression<Func<User, bool>>? filter = null, Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = null, List<Func<IQueryable<User>, IIncludableQueryable<User, object>>>? includes = null)
    {
        throw new NotImplementedException();
    }
}