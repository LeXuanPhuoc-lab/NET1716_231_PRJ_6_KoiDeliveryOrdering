using KoiDeliveryOrdering.Business.Base;
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
    }
}
