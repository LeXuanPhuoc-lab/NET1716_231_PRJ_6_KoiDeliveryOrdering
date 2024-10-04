using KoiDeliveryOrdering.Data.Entities;

namespace KoiDeliveryOrdering.MVCWebApp.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateTime? DateOfBirth { get; set; }

        public string Phone { get; set; } = null!;

        public string? AvatarImage { get; set; }

        public string? IdentityCard { get; set; }

        public DateTime? CreateDate { get; set; }

        public string? Address { get; set; }

        public double? Longitude { get; set; }

        public double? Latitude { get; set; }

        public string? Username { get; set; }

        public string Password { get; set; } = null!;

        public bool? IsActive { get; set; }

        //public virtual ICollection<DeliveryOrderModel> DeliveryOrders { get; set; } = new List<DeliveryOrderModel>();

        //public virtual ICollection<SenderInformationModel> SenderInformations { get; set; } = new List<SenderInformationModel>();
    }
}
