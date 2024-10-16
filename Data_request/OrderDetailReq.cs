using intern_prj.Entities;

namespace intern_prj.Data_request
{
    public class OrderDetailReq
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public virtual productReq Product { get; set; } = null!;

        public int? UnitQuantity { get; set; }
    }
}
