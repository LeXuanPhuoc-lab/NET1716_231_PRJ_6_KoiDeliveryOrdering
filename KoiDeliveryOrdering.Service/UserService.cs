using System.Linq.Expressions;
using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Business.Interfaces;
using KoiDeliveryOrdering.Common;
using KoiDeliveryOrdering.Data;
using KoiDeliveryOrdering.Data.Dtos;
using KoiDeliveryOrdering.Data.Entities;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace KoiDeliveryOrdering.Business;

public class UserService(UnitOfWork unitOfWork, IMapper mapper) : IUserService
{
    public async Task<IServiceResult> InsertAsync(UserDto test)
    {
        try
        {
            // Prepare insert 
            await unitOfWork.UserRepository.PrepareInsertAsync(mapper.Map<User>(test));
            
            // Perform insert query to db
            var isInserted = await unitOfWork.UserRepository.SaveChangeWithTransactionAsync() > 0;

            return isInserted
                // Insert successfully
                ? new ServiceResult(Const.SUCCESS_INSERT_CODE, Const.SUCCESS_INSERT_MSG)
                // Insert error
                : new ServiceResult(Const.FAIL_INSERT_CODE, Const.FAIL_INSERT_MSG);
        }
        catch (Exception ex)
        {
            // Invoke error
            return new ServiceResult(Const.ERROR_EXCEPTION_CODE, ex.Message);
        }
    }

    public async Task<IServiceResult> RemoveAsync(Guid userId)
    {
        try
        {
            // Get by id 
            var userEntity = await unitOfWork.UserRepository.FindOneWithConditionAsync(u => 
                u.UserId == userId);
            
            if(userEntity == null) return new ServiceResult(Const.FAIL_REMOVE_CODE, Const.FAIL_REMOVE_MSG);
        
            // Prepare remove 
            await unitOfWork.UserRepository.PrepareRemoveAsync(userEntity);
        
            // Save to db
            var isRemoved = await unitOfWork.UserRepository.SaveChangeWithTransactionAsync() > 0;
        
            return isRemoved
                // Delete successfully
                ? new ServiceResult(Const.SUCCESS_REMOVE_CODE, Const.SUCCESS_REMOVE_MSG)
                // Delete fail
                : new ServiceResult(Const.FAIL_REMOVE_CODE, Const.FAIL_REMOVE_MSG);
        }
        catch (Exception ex)
        {
            // Invoke error
            return new ServiceResult(Const.ERROR_EXCEPTION_CODE, ex.Message);
        }
    }
    
    public async Task<IServiceResult> UpdateAsync(UserDto user)
    {
        try
        {
            // Get by id 
            var userEntity = await unitOfWork.UserRepository.FindOneWithConditionAsync(u => 
                u.UserId == user.UserId);
        
            if(userEntity == null) return new ServiceResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
        
            // Update entity properties 
            userEntity.Address = user.Address;
            
            await unitOfWork.UserRepository.UpdateAsync(userEntity, saveChanges: false);
        
            // Save to db
            var isUpdated = await unitOfWork.UserRepository.SaveChangeWithTransactionAsync() > 0;
        
            return isUpdated
                // Is delete success
                ? new ServiceResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG)
                // Invoke error
                : new ServiceResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);
        }catch (Exception ex)
        {
            // Invoke error
            return new ServiceResult(Const.ERROR_EXCEPTION_CODE, ex.Message);
        }
    }

    public Task<IServiceResult> FindAsync(Guid userId)
    {
        throw new NotImplementedException();
	}

    public async Task<IServiceResult> FindAllAsync()
    {
        try
        {
            var userEntities = 
                await unitOfWork.UserRepository.FindAllWithConditionAndThenIncludeAsync(
                    filter: null,
                    orderBy: null,
                    includes: new ()
                    {
                        query => query.Include(u => u.SenderInformations)
                    });

            if (userEntities.Any())
            {
                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, userEntities);
            }
            
            return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new List<User>());
        }
        catch (Exception ex)
        {
            return new ServiceResult(Const.ERROR_EXCEPTION_CODE, ex.Message);
        }
    }

    public Task<IServiceResult> FindOneWithConditionAsync(Expression<Func<User, User>>? filter, Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = null, string? includeProperties = "")
    {
        throw new NotImplementedException();
    }

    public Task<IServiceResult> FindAllWithConditionAsync(Expression<Func<User, User>>? filter = null, Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = null, string? includeProperties = "")
    {
        throw new NotImplementedException();
    }

    public Task<IServiceResult> FindAllWithConditionAndThenIncludeAsync(Expression<Func<User, bool>>? filter = null, Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = null, List<Func<IQueryable<User>, IIncludableQueryable<User, object>>>? includes = null)
    {
        throw new NotImplementedException();
    }

	public async Task<IServiceResult> FindAllSenderInformationAsync()
	{
        try
        {
            var senderInformations = await unitOfWork.UserRepository.FindAllSenderInformationAsync();

            if (senderInformations.Any())
            {
                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, senderInformations);
            }

            return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new List<SenderInformation>());

        }
        catch (Exception ex)
        {
            return new ServiceResult(Const.ERROR_EXCEPTION_CODE, ex.Message);
        }
    }

    public async Task<IServiceResult> FindByUsernameAsync(string username)
    {
        try
        {
            // Get by username
            var userEntity = await unitOfWork.UserRepository.FindOneWithConditionAsync(u =>
                u.Username == username);

            return userEntity != null
                // Get successfully
                ? new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, userEntity)
                // Get fail
                : new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new User());
        }
        catch (Exception ex)
        {
            // Invoke error
            return new ServiceResult(Const.ERROR_EXCEPTION_CODE, ex.Message);
        }
    }

    public async Task<IServiceResult> FindAllVoucherByUsernameAsync(string username)
    {
        try
        {
            // Get by username
            var voucherPromotions = await unitOfWork.UserRepository.FindAllVoucherByUsernameAsync(username);

            return voucherPromotions.Any()
                // Get successfully
                ? new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, voucherPromotions)
                // Get fail
                : new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new List<VoucherPromotion>());
        }
        catch (Exception ex)
        {
            // Invoke error
            return new ServiceResult(Const.ERROR_EXCEPTION_CODE, ex.Message);
        }
    }
}