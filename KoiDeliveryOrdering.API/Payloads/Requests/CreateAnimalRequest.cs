using KoiDeliveryOrdering.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace KoiDeliveryOrdering.API.Payloads.Requests;

public class CreateAnimalRequest
{
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

public static class CreateAnimalRequestExtension 
{
    public static List<DeliveryOrderDetail> ToListDeliveryOrderDetail(
        this List<CreateAnimalRequest> animalRequests)
    {
        return animalRequests.Select(ar => new DeliveryOrderDetail
        {
            PreDeliveryHealthStatus = ar.HealthStatus,
            Animal = new()
            {
                Breed = ar.Breed,
                ColorPattern = ar.ColorPattern,
                Size = ar.Size,
                Age = ar.Age,
                EstimatedPrice = ar.EstimatedPrice,
                HealthStatus = ar.HealthStatus,
                IsAvailable = ar.IsAvailable,
                OriginCountry = ar.OriginCountry,
                Description = ar.Description,
                ImageUrl = ar.ImageUrl,
                AnimalTypeId = ar.AnimalTypeId
            }
        }).ToList();
    }    
}
