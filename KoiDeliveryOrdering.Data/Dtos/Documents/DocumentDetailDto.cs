using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KoiDeliveryOrdering.Data.Dtos.Documents;

public class DocumentDetailDto
{
    public string? DocumentDetailDescription { get; set; }

    [Required]
    [DisplayName("Item name")]
    public string ItemName { get; set; } = null!;

    [Required]
    [DisplayName("Weight")]
    public decimal ItemWeight { get; set; }

    [Required]
    [DisplayName("Item category")]
    public string ItemCategory { get; set; } = null!;
    
    public decimal ItemEstimatePrice { get; set; }

    [Required]
    [DisplayName("Quantity")]
    public int ItemQuantity { get; set; }
}