using System.Text.Json.Serialization;

namespace KoiDeliveryOrdering.MVCWebApp.Models
{
    public class AnimalTypeModel
    {
        public int AnimalTypeId { get; set; }

        public string? AnimalTypeDesc { get; set; }

        [JsonIgnore]
        public virtual ICollection<AnimalModel> Animals { get; set; } = new List<AnimalModel>();
    }
}
