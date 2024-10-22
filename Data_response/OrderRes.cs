using intern_prj.Entities;

namespace intern_prj.Data_response
{
    public class OrderRes
    {
        public string? UserId { get; set; }
        public string? OrderCode { get; set; }
        public string? RecipientName { get; set; }
        public string? RecipientAddress { get; set; }
        public string? RecipientPhone { get; set; }
        public string? RecipientEmail { get; set; }

        public DateTime? OrderDate { get; set; }

        public decimal? TotalAmount { get; set; }

        public string? StatusPayment { get; set; } = "Chưa thanh toán";
        public string? StatusShipping { get; set; } = "Đang chờ";

        public string? PaymentMethod { get; set; } = string.Empty;

        public ICollection<OrderDetailRes> OrderDetails { get; set; } = new List<OrderDetailRes>();
        public virtual ApplicationUser? User { get; set; }
    }
}
