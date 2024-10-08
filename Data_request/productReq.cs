using intern_prj.Entities;
using System.Text.Json.Serialization;

namespace intern_prj.Data_request
{
    public class productReq
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
        public virtual ICollection<imageReq> Images { get; set; } = new List<imageReq>();
    }
}
