using KoiDeliveryOrdering.Data.Entities;

namespace KoiDeliveryOrdering.API.Payloads.Requests
{
    public class UpdateDeliveryOrderDetailRequest
    {
        public int Id { get; set; }

        public Guid DeliveryOrderDetailId { get; set; }

        public int AnimalId { get; set; }

        public int DeliveryOrderId { get; set; }

        public string? PreDeliveryHealthStatus { get; set; }

        public string? PostDeliveryHealthStatus { get; set; }

        public virtual UpdateAnimalRequest Animal { get; set; } = null!;
    }

    public static class UpdateDeliveryOrderDetailExtension
    {
        public static List<DeliveryOrderDetail> ToListDeliveryOrderDetail(
            this List<UpdateDeliveryOrderDetailRequest> updateOrderDetailRequests)
        {
            return updateOrderDetailRequests.Select(od => new DeliveryOrderDetail
            {
                DeliveryOrderDetailId = od.DeliveryOrderDetailId,
                Id = od.Id,
                DeliveryOrderId = od.DeliveryOrderId,
                AnimalId = od.AnimalId,
                Animal = new()
                {
                    Id = od.Animal.Id,
                    AnimalId = od.Animal.AnimalId,
                    Breed = od.Animal.Breed,
                    ColorPattern = od.Animal.ColorPattern,
                    Size = od.Animal.Size,
                    Age = od.Animal.Age,
                    EstimatedPrice = od.Animal.EstimatedPrice,
                    HealthStatus = od.Animal.HealthStatus,
                    IsAvailable = od.Animal.IsAvailable,
                    OriginCountry = od.Animal.OriginCountry,
                    Description = od.Animal.Description,
                    ImageUrl = od.Animal.ImageUrl,
                    AnimalTypeId = od.Animal.AnimalTypeId
                },
                PreDeliveryHealthStatus = od.Animal.HealthStatus,
                PostDeliveryHealthStatus = od.PostDeliveryHealthStatus
            }).ToList();
        }
    }
}
