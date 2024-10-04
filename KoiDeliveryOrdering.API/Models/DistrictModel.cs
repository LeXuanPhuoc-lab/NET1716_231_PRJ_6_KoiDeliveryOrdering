namespace KoiDeliveryOrdering.API.Models;

public class DistrictModel
{
    public string Name { get; set; } = string.Empty;
    public int Code { get; set; }
    public string DivisionType { get; set; } = string.Empty;
    public string CodeName { get; set; } = string.Empty;
    public int PhoneCode { get; set; }
    public List<WardModel> Wards { get; set; } = new();
}
