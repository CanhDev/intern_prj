using intern_prj.Data_response;

namespace intern_prj.Data_request
{
    public class OrderReq
    {
        public int Id { get; set; }

        public string? UserId { get; set; }
        public string? OrderCode { get; set; }

        public DateTime? OrderDate { get; set; }

        public decimal? TotalAmount { get; set; }

        public string? Status { get; set; } = "pending";

        public string? PaymentMethod { get; set; } = string.Empty;

        public string? ShippingAddress { get; set; } = string.Empty;
        public ICollection<OrderDetailReq> OrderDetails { get; set; } = new List<OrderDetailReq>();
    }
}
