namespace KoiDeliveryOrdering.API.Models
{
    public class GHNDistrictModel
    {
        public int DistrictId { get; set; }
        public int ProvinceId { get; set; }
        public string DistrictName { get; set; } = string.Empty;
        public int SupportType { get; set; }
        public string[] NameExtension { get; set; } = null!;
        public bool CanUpdateCOD { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
