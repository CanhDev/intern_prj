using System;
using System.Collections.Generic;

namespace intern_prj.Entities;

public partial class FeedBack
{
    public int Id { get; set; }

    public int? OrderId { get; set; }
    public string? roleName { get; set; }

    public string? Description { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual Order? Order { get; set; }
}
