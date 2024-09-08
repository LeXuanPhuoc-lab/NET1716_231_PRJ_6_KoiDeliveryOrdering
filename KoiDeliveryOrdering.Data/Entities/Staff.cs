using System;
using System.Collections.Generic;

namespace KoiDeliveryOrdering.Data.Entities;

public partial class Staff
{
    public int Id { get; set; }

    public Guid StaffId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime? DateOfBirth { get; set; }

    public string Phone { get; set; } = null!;

    public string? AvatarImage { get; set; }

    public string? IdentityCard { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? JobTitleId { get; set; }

    public string? Address { get; set; }

    public double? Longitude { get; set; }

    public double? Latitude { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<CareLog> CareLogs { get; set; } = new List<CareLog>();

    public virtual JobTitle? JobTitle { get; set; }

    public virtual ICollection<OrderAssignment> OrderAssignmentDrivers { get; set; } = new List<OrderAssignment>();

    public virtual ICollection<OrderAssignment> OrderAssignmentFishCarers { get; set; } = new List<OrderAssignment>();
}
