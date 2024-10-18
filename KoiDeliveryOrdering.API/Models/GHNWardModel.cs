namespace KoiDeliveryOrdering.API.Models
{
    public class GHNWardModel
    {
        public int WardCode { get; set; }
        public int DistrictId { get; set; }
        public string WardName { get; set; } = string.Empty;
        public string[] NameExtension { get; set; } = null!;
        public bool CanUpdateCOD { get; set; }
        public int SupportType { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
