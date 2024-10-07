using intern_prj.Entities;

namespace intern_prj.Data_response
{
    public class OrderRes
    {
        public string? UserId { get; set; }
        public string? OrderCode { get; set; }

        public DateTime? OrderDate { get; set; }

        public decimal? TotalAmount { get; set; }

        public string? Status { get; set; } = "pending";

        public string? PaymentMethod { get; set; } = string.Empty;

        public string? ShippingAddress { get; set; } = string.Empty;
        public ICollection<OrderDetailRes> OrderDetails { get; set; } = new List<OrderDetailRes>();
    }
}
