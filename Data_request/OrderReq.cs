using intern_prj.Data_response;

namespace intern_prj.Data_request
{
    public class OrderReq
    {
        public int Id { get; set; }

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

        public string? ShippingAddress { get; set; } = string.Empty;
        public ICollection<OrderDetailReq> OrderDetails { get; set; } = new List<OrderDetailReq>();
    }
}
