using System;
using System.Collections.Generic;

namespace intern_prj.Entities;

public partial class OrderDetail
{
    public int Id { get; set; }

    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public decimal UnitPrice { get; set; }

    public int? UnitQuantity { get; set; }
    public virtual Order Order { get; set; } = null;
    public virtual Product Product { get; set; } = null;
}
