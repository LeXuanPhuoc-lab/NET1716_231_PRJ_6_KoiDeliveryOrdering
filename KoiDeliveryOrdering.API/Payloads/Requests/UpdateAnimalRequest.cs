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
}
