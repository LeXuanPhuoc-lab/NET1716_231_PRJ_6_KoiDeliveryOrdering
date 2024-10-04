namespace KoiDeliveryOrdering.Data.Entities;

public partial class DailyCareSchedule
{
    public int DailyCareScheduleId { get; set; }

    public int CareTaskId { get; set; }

    public string? TaskFrequency { get; set; } // Tần suất nhiệm vụ (hàng ngày, hàng tuần, v.v.)

    public decimal? RecommendedValue { get; set; } // Giá trị đề xuất (ví dụ: liều lượng thức ăn, lượng nước)

    public DateTime? StartDate { get; set; } // Ngày bắt đầu nhiệm vụ chăm sóc

    public DateTime? EndDate { get; set; } // Ngày kết thúc nhiệm vụ chăm sóc

    public string? TaskDuration { get; set; } // Thời gian thực hiện nhiệm vụ (ví dụ: 30 phút, 1 giờ)

    public string? Notes { get; set; } // Ghi chú thêm cho lịch trình chăm sóc

    public bool IsCritical { get; set; } // Xác định xem nhiệm vụ có quan trọng không (ví dụ: nhiệm vụ cấp bách)

    public int DeliverOrderDetailId { get; set; } // ID của chi tiết đơn hàng giao

    public string? CaregiverName { get; set; } // Tên người chăm sóc

    public DateTime? LastPerformedDate { get; set; } // Ngày lần cuối nhiệm vụ được thực hiện

    public virtual ICollection<CareLog> CareLogs { get; set; } = new List<CareLog>();

    public virtual CareTask CareTask { get; set; } = null!;

    public virtual DeliveryOrderDetail DeliverOrderDetail { get; set; } = null!;
}

