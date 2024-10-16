using System;
using System.Collections.Generic;

namespace intern_prj.Entities;

public partial class Order
{
    public int Id { get; set; }

    public string? UserId { get; set; }
    public string? RecipientName { get; set; }
    public string? RecipientAddress {  get; set; }
    public string? RecipientPhone { get; set; }
    public string? RecipientEmail {  get; set; }
    public string? OrderCode {  get; set; }

    public DateTime? OrderDate { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? StatusPayment { get; set; } = "Chưa thanh toán";
    public string? StatusShipping { get; set; } = "Đang chờ";

    public string? PaymentMethod { get; set; } = string.Empty;

    public string? ShippingAddress { get; set; } = string.Empty;

    public virtual ICollection<FeedBack> FeedBacks { get; set; } = new List<FeedBack>();
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ApplicationUser? User { get; set; }
}
