using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace intern_prj.Entities;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public long Quantity { get; set; }

    public decimal OriginalPrice { get; set; }

    public decimal Price { get; set; }

    public DateTime? CreateDate { get; set; } = null;
    public DateTime? UpdateDate { get; set; } = null;

    public int? CategoryId { get; set; }

    public string? Size { get; set; }

    public int? SoldedCount { get; set; }
    public string? Manufacturer { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<ItemCart>? ItemCarts { get; set; }
}
