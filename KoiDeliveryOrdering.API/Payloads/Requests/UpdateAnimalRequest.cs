using KoiDeliveryOrdering.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace KoiDeliveryOrdering.API.Payloads.Requests
{
    public class UpdateAnimalRequest
    {
        public int Id { get; set; }
        public Guid AnimalId { get; set; }

        public string? Breed { get; set; }

        public string? ColorPattern { get; set; }

        public decimal? Size { get; set; }

        public int? Age { get; set; }

        public decimal? EstimatedPrice { get; set; }

        public string? HealthStatus { get; set; }

        public bool? IsAvailable { get; set; }

        public string? OriginCountry { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        public int AnimalTypeId { get; set; }
    }
    public static class UpdateAnimalExtension 
    {
        public static Animal ToAnimal(this UpdateAnimalRequest request)
        {
            var animal = new Animal()
            {
                Id = request.Id,
                HealthStatus = request.HealthStatus,
                Age = request.Age,
                AnimalTypeId = request.AnimalTypeId,
                Breed = request.Breed,
                ColorPattern = request.ColorPattern,
                Size = request.Size,
                Description = request.Description,
                EstimatedPrice = request.EstimatedPrice,
                ImageUrl = request.ImageUrl,
                IsAvailable = request.IsAvailable,
                OriginCountry = request.OriginCountry,
            };
            return animal;
        }
    }
}
