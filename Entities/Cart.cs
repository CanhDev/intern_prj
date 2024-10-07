using System;
using System.Collections.Generic;

namespace intern_prj.Entities;

public partial class Cart
{
    public int Id { get; set; }

    public int ProductTypeQuan { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? UserId { get; set; }

    public virtual ICollection<ItemCart> ItemCarts { get; set; } = new List<ItemCart>();

    public virtual ApplicationUser? User { get; set; }
}
