using KoiDeliveryOrdering.Business.Base;

namespace KoiDeliveryOrdering.Business.Interfaces
{
    public interface IAnimalService
    {
        Task<IServiceResult> FindAllAnimalTypeAsync();
        Task<IServiceResult> FindAllHealthStatusAsync();
    }
}
