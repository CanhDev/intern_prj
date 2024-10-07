using System;
using System.Collections.Generic;

namespace intern_prj.Entities;

public partial class Image
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public string? ImageUrl { get; set; }

    public virtual Product? Product { get; set; }
}
