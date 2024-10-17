using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Data.Entities;

namespace KoiDeliveryOrdering.Business.Interfaces
{
    public interface IAnimalService
    {
        Task<IServiceResult> FindAllAnimalTypeAsync();
        Task<IServiceResult> FindAllHealthStatusAsync();
        Task<IServiceResult> UpdateAsync(Animal animal);
        Task<IServiceResult> RemoveAsync(int id);
        Task<IServiceResult> GetAllAnimal();
        Task<IServiceResult> FindAnimalById(int id);
        Task<IServiceResult> InsertAsync(Animal animal);
    }
}
