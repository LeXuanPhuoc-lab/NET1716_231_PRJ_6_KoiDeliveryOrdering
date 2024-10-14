﻿using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Business.Contants;
using KoiDeliveryOrdering.Business.Interfaces;
using KoiDeliveryOrdering.Common;
using KoiDeliveryOrdering.Data;
using KoiDeliveryOrdering.Data.Entities;

namespace KoiDeliveryOrdering.Business
{
    public class AnimalService(UnitOfWork unitOfWork) : IAnimalService
    {
        public async Task<IServiceResult> FindAllAnimalTypeAsync()
        {
            try
            {
                var animalTypes = await unitOfWork.AnimalRepository.FindAllAnimalTypeAsync();

                if(animalTypes.Any())
                {
                    return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, animalTypes);
                }

                return new ServiceResult(Const.FAIL_READ_CODE, Const.FAIL_READ_MSG, new List<AnimalType>());

            }catch(Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION_CODE, ex.Message);
            }
        }

        public async Task<IServiceResult> FindAllHealthStatusAsync()
        {
            List<string> healthStatuses = new()
            {
                HealthStatusConstants.Good,
                HealthStatusConstants.Sick,
                HealthStatusConstants.UnderObservation,
            };

            return await Task.FromResult(
                new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, healthStatuses));
        }

        public async Task<IServiceResult> RemoveAsync(int id)
        {
            try
            {
                // Get animal by id 
                var toUpdateAnimal = await unitOfWork.AnimalRepository.FindAsync(id);
                if (toUpdateAnimal == null) return new ServiceResult(Const.FAIL_REMOVE_CODE, Const.FAIL_REMOVE_MSG, false);

                await unitOfWork.AnimalRepository.PrepareRemoveAsync(toUpdateAnimal.Id);
                var isRemoved = await unitOfWork.DeliveryOrderRepository.SaveChangeWithTransactionAsync() > 0;

                if (!isRemoved)
                {
                    return new ServiceResult(Const.FAIL_REMOVE_CODE, Const.FAIL_REMOVE_MSG, false);
                }

                return new ServiceResult(Const.SUCCESS_REMOVE_CODE, Const.SUCCESS_REMOVE_MSG, true);
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION_CODE, ex.Message);
            }
        }

        public async Task<IServiceResult> UpdateAsync(Animal animal)
        {
            try
            {
                // Get animal by id 
                var toUpdateAnimal = await unitOfWork.AnimalRepository.FindAsync(animal.Id);
                if (toUpdateAnimal == null) return new ServiceResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG, false);

                //Update animal properties
                toUpdateAnimal.Breed = animal.Breed;
                toUpdateAnimal.ColorPattern = animal.ColorPattern;
                toUpdateAnimal.Size = animal.Size;
                toUpdateAnimal.Age = animal.Age;
                toUpdateAnimal.EstimatedPrice = animal.EstimatedPrice;
                toUpdateAnimal.HealthStatus = animal.HealthStatus;
                toUpdateAnimal.IsAvailable = animal.IsAvailable;
                toUpdateAnimal.OriginCountry = animal.OriginCountry;
                toUpdateAnimal.Description = animal.Description;
                toUpdateAnimal.ImageUrl = animal.ImageUrl;
                toUpdateAnimal.AnimalTypeId = animal.AnimalTypeId;

                unitOfWork.AnimalRepository.PrepareUpdate(toUpdateAnimal);
                var isUpdated = await unitOfWork.DeliveryOrderRepository.SaveChangeWithTransactionAsync() > 0;

                if (!isUpdated)
                {
                    return new ServiceResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG, false);
                }

                return new ServiceResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG, true);
            }
            catch(Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION_CODE, ex.Message);
            }
        }
    }
}
