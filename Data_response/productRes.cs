﻿using intern_prj.Data_request;

namespace intern_prj.Data_response
{
    public class productRes
    {

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public long Quantity { get; set; }

        public decimal OriginalPrice { get; set; }

        public decimal Price { get; set; }
        public bool? OutOfStockstatus { get; set; } = false;

        public DateTime? CreateDate { get; set; } = null;
        public DateTime? UpdateDate { get; set; } = null;

        public int? CategoryId { get; set; }

        public string? Size { get; set; }

        public int? SoldedCount { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();
        public List<IFormFile>? imgs { get; set; }
    }
}
