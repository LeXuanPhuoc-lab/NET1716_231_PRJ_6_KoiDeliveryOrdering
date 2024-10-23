namespace KoiDeliveryOrdering.API.Models
{
    public class GHNProvinceModel
    {
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; } = string.Empty;
        public int CountryId { get; set; }
        public string[] NameExtension { get; set; } = null!;
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public bool IsEnable { get; set; }
        public bool RegionId { get; set; }
        public bool CanUpdateCOD { get; set; }
        public int Status { get; set; }
    }
}
