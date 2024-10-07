namespace intern_prj.Data_request
{
    public class OrderDetailReq
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }

        public int? UnitQuantity { get; set; }
    }
}
