using System;
using System.Collections.Generic;

namespace intern_prj.Entities;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? imageUrl { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
